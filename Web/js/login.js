document.getElementById("loginForm").addEventListener("submit", async function (e) {
  e.preventDefault();

  const email = document.getElementById("email").value;
  const senha = document.getElementById("senha").value;

  const dados = { email, senha };

  try {
    const resposta = await fetch("https://localhost:7049/api/Usuario/login", {
      method: "POST",
      headers: {
        "Content-Type": "application/json"
      },
      body: JSON.stringify(dados)
    });

    const resultado = await resposta.json();

    if (!resposta.ok) {
      document.getElementById("mensagem").textContent = resultado || "Erro ao fazer login";
    } else {
      document.getElementById("mensagem").textContent = "Login realizado com sucesso!";
      console.log("Usuário logado:", resultado);

      // Redirecionar para a página index.html após login bem-sucedido
      window.location.href = "/html/index.html";  // ajuste o caminho conforme a estrutura do seu projeto
    }
  } catch (erro) {
    document.getElementById("mensagem").textContent = "Erro ao conectar com a API.";
    console.error("Erro:", erro);
  }
});
