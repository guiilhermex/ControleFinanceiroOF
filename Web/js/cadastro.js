document.getElementById("formCadastro").addEventListener("submit", async function (event) {
    event.preventDefault();
  
    const nome = document.getElementById("nome").value;
    const email = document.getElementById("email").value;
    const senha = document.getElementById("senha").value;
  
    const usuario = {
      nome: nome,
      email: email,
      senha: senha
    };
  
    try {
      const response = await fetch("https://localhost:7049/api/Usuario", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(usuario)
      });
  
      if (response.ok) {
        document.getElementById("mensagem").textContent = "Usuário cadastrado com sucesso!";
      } else {
        const erro = await response.json();
        document.getElementById("mensagem").textContent = "Erro: " + JSON.stringify(erro);
      }
    } catch (err) {
      console.error("Erro na requisição:", err);
      document.getElementById("mensagem").textContent = "Erro ao conectar com a API.";
    }
  });
  