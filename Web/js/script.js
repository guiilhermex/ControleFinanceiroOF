document.addEventListener('DOMContentLoaded', () => {
    const apiBaseUrl = "https://localhost:7049/api";
    const usuarioId = 1;

    const formatarData = (data) => new Date(data).toLocaleDateString('pt-BR');
    const formatarMoeda = (valor) => `R$ ${parseFloat(valor).toFixed(2)}`;

    // ========== RECEITAS ========== //
    async function carregarReceitas() {
        try {
            const res = await fetch(`${apiBaseUrl}/Receita/usuario/${usuarioId}`);
            const receitas = await res.json();

            const tabelaFixas = document.querySelector("#tabela-receitas-fixas tbody");
            const tabelaExtras = document.querySelector("#tabela-receitas-extras tbody");
            const totalFixasSpan = document.getElementById("total-receitas-fixas");
            const totalExtrasSpan = document.getElementById("total-receitas-extras");

            tabelaFixas.innerHTML = "";
            tabelaExtras.innerHTML = "";
            console.log(tabelaFixas, tabelaExtras);
            let totalFixas = 0;
            let totalExtras = 0;

            receitas.forEach(r => {
                const tr = document.createElement("tr");
                tr.innerHTML = `
          <td>${r.id}</td>
          <td>${r.descricao}</td>
          <td>${formatarMoeda(r.valor)}</td>
          <td>${formatarData(r.data)}</td>
          <td>
            <button onclick="editarReceita(${r.id})">Editar</button>
            <button onclick="excluirReceita(${r.id})">Excluir</button>
          </td>
        `;
                console.log(tr, r);

                if (!r.extra) {
                    tabelaFixas.appendChild(tr);
                    totalFixas += r.valor;
                } else {
                    tabelaExtras.appendChild(tr);
                    totalExtras += r.valor;
                }
            });

            totalFixasSpan.textContent = formatarMoeda(totalFixas);
            totalExtrasSpan.textContent = formatarMoeda(totalExtras);
        } catch (e) {
            console.error("Erro ao carregar receitas", e);
        }
    }

    async function adicionarOuEditarReceita(tipo) {
        const extra = tipo === "EXTRA";
        const tipoMin = tipo.toLowerCase();
        const id = document.getElementById(`id-receita-${tipoMin}`).value;
        const descricao = document.getElementById(`descricao-receita-${tipoMin}`).value.trim();
        const valor = parseFloat(document.getElementById(`valor-receita-${tipoMin}`).value) || 0;
        const data = document.getElementById(`data-receita-${tipoMin}`).value;

        const receita = { descricao, valor, data, extra, usuarioId };

        try {
            const url = id ? `${apiBaseUrl}/Receita/${id}` : `${apiBaseUrl}/Receita`;
            const metodo = id ? "PUT" : "POST";

            await fetch(url, {
                method: metodo,
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(id ? { ...receita, id } : receita)
            });

            document.getElementById(`form-receita-${tipoMin}`).reset();
            document.getElementById(`id-receita-${tipoMin}`).value = "";
            carregarReceitas();
        } catch (e) {
            console.error("Erro ao salvar receita", e);
        }
    }

    window.editarReceita = async function (id) {
        try {
            const res = await fetch(`${apiBaseUrl}/Receita/${id}`);
            const r = await res.json();
            const tipoMin = r.tipo.toLowerCase();

            document.getElementById(`id-receita-${tipoMin}`).value = r.id;
            document.getElementById(`descricao-receita-${tipoMin}`).value = r.descricao;
            document.getElementById(`valor-receita-${tipoMin}`).value = r.valor;
            document.getElementById(`data-receita-${tipoMin}`).value = r.data.split("T")[0];
        } catch (e) {
            console.error("Erro ao editar receita", e);
        }
    };

    window.excluirReceita = async function (id) {
        try {
            await fetch(`${apiBaseUrl}/Receita/${id}`, { method: "DELETE" });
            carregarReceitas();
        } catch (e) {
            console.error("Erro ao excluir receita", e);
        }
    };

    document.getElementById("form-receita-fixa").addEventListener("submit", (e) => {
        e.preventDefault();
        adicionarOuEditarReceita("FIXA");
    });

    document.getElementById("form-receita-extra").addEventListener("submit", (e) => {
        e.preventDefault();
        adicionarOuEditarReceita("EXTRA");
    });

    // ========== DESPESAS ========== //
    async function listarDespesas() {
        try {
            const res = await fetch(`${apiBaseUrl}/Despesa/usuario/${usuarioId}`);
            const despesas = await res.json();

            const tbody = document.querySelector("#tabela-despesas tbody");
            tbody.innerHTML = "";

            despesas.forEach(d => {
                const tr = document.createElement("tr");
                tr.innerHTML = `
          <td>${d.id}</td>
          <td>${d.nome}</td>
          <td>${formatarMoeda(d.valor)}</td>
          <td>${d.categoria}</td>
          <td>${formatarData(d.data)}</td>
          <td>${d.descricao}</td>
          <td>
            <button class="botao-editar" onclick="editarDespesa(${d.id})">Editar</button>
            <button class="botao-excluir" onclick="excluirDespesa(${d.id})">Excluir</button>
          </td>
        `;
                tbody.appendChild(tr);
            });
        } catch (e) {
            console.error("Erro ao listar despesas", e);
        }
    }

    async function adicionarDespesa() {
        const id = document.getElementById("id-despesa").value;
        const nome = document.getElementById("nome-despesa").value.trim();
        const valor = parseFloat(document.getElementById("valor-despesa").value);
        const categoria = document.getElementById("categoria-despesa").value.trim();
        const data = document.getElementById("data-despesa").value;
        const descricao = document.getElementById("descricao-despesa").value.trim();

        const despesa = { nome, valor, categoria, data, descricao, usuarioId };

        try {
            const url = id ? `${apiBaseUrl}/Despesa/${id}` : `${apiBaseUrl}/Despesa`;
            const metodo = id ? "PUT" : "POST";

            await fetch(url, {
                method: metodo,
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(id ? { ...despesa, id } : despesa)
            });

            document.getElementById("form-despesa").reset();
            document.getElementById("id-despesa").value = "";
            listarDespesas();
        } catch (e) {
            console.error("Erro ao salvar despesa", e);
        }
    }

    window.editarDespesa = async function (id) {
        try {
            const res = await fetch(`${apiBaseUrl}/Despesa/${id}`);
            const d = await res.json();

            document.getElementById("id-despesa").value = d.id;
            document.getElementById("nome-despesa").value = d.nome;
            document.getElementById("valor-despesa").value = d.valor;
            document.getElementById("categoria-despesa").value = d.categoria;
            document.getElementById("data-despesa").value = d.data.split("T")[0];
            document.getElementById("descricao-despesa").value = d.descricao;
        } catch (e) {
            console.error("Erro ao editar despesa", e);
        }
    };

    window.excluirDespesa = async function (id) {
        try {
            await fetch(`${apiBaseUrl}/Despesa/${id}`, { method: "DELETE" });
            listarDespesas();
        } catch (e) {
            console.error("Erro ao excluir despesa", e);
        }
    };

    document.getElementById("form-despesa").addEventListener("submit", (e) => {
        e.preventDefault();
        adicionarDespesa();
    });

    // Inicialização
    carregarReceitas();
    listarDespesas();
});
