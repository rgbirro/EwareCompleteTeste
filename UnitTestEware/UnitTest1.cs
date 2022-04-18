using EwareTeste.Model;
using EwareTeste.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using Xunit;

namespace UnitTest
{
    public class UnitTest1
    {
        [Fact]
        public async void Test_salvar_caminhao()
        {
            //Arrange
            var context = new EwareTesteContext();
            var createModel = new CreateModel(context);

            //Act
            createModel.Caminhao = new();
            createModel.Caminhao.Nome = "Teste Nome"; 
            createModel.Caminhao.Descricao = "Teste Descrição";
            createModel.Caminhao.ModeloId = 1;
            createModel.Caminhao.AnoFabricacao = 2022;
            createModel.Caminhao.AnoModelo = 2023;
            await createModel.OnPostAsync();

            //Assert
            Assert.NotNull(context.Caminhoes.Where(c => c.Descricao == "Teste Descrição").First());
        }

        [Fact]
        public async void Test_salvar_modelo_caminhao_invalido()
        {
            //Arrange
            var context = new EwareTesteContext();
            var createModel = new CreateModel(context);

            //Act
            createModel.Caminhao = new(); 
            createModel.Caminhao.Nome = "Teste Nome";
            createModel.Caminhao.Descricao = "Teste Descrição";
            createModel.Caminhao.ModeloId = 3;
            createModel.Caminhao.AnoFabricacao = 2022;
            createModel.Caminhao.AnoModelo = 2023;
            await createModel.OnPostAsync();

            //Assert
            Assert.NotNull(createModel.ErroValidacaoForm);
        }

        [Fact]
        public async void Test_excluir_caminhao_null()
        {
            //Arrange
            var context = new EwareTesteContext();
            var deleteModel = new DeleteModel(context);

            //Act            
            var result = await deleteModel.OnPostAsync(null);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void Test_excluir_caminhao_correto()
        {
            //Arrange
            var context = new EwareTesteContext();
            var createModel = new CreateModel(context);
            var deleteModel = new DeleteModel(context);

            //Act
            createModel.Caminhao = new();
            createModel.Caminhao.Nome = "Teste Nome";
            createModel.Caminhao.Descricao = "Teste Descrição";
            createModel.Caminhao.ModeloId = 1;
            createModel.Caminhao.AnoFabricacao = 2022;
            createModel.Caminhao.AnoModelo = 2023;
            await createModel.OnPostAsync();
            int idToDelete = context.Caminhoes.Where(c => c.Descricao == "Teste Descrição").First().Id;
            await deleteModel.OnPostAsync(idToDelete);

            //Assert
            Assert.Null(context.Caminhoes.Where(c => c.Id == idToDelete).FirstOrDefault());
        }

        [Fact]
        public async void Test_editar_caminhao_correto()
        {
            //Arrange
            var context = new EwareTesteContext();
            var createModel = new CreateModel(context);
            var editModel = new EditModel(context);

            //Act
            createModel.Caminhao = new();
            createModel.Caminhao.Nome = "Teste Nome";
            createModel.Caminhao.Descricao = "Teste Descrição";
            createModel.Caminhao.ModeloId = 1;
            createModel.Caminhao.AnoFabricacao = 2022;
            createModel.Caminhao.AnoModelo = 2023;
            await createModel.OnPostAsync();
            editModel.Caminhao = createModel.Caminhao;
            editModel.Caminhao.Nome = "Teste Nome Editado";
            editModel.Caminhao.Descricao = "Teste Descrição Editado";
            editModel.Caminhao.ModeloId = 2;
            editModel.Caminhao.AnoFabricacao = 2022;
            editModel.Caminhao.AnoModelo = 2022;
            await editModel.OnPostAsync();

            //Assert
            Assert.NotNull(context.Caminhoes
                .Where(c => c.Nome.Equals("Teste Nome Editado"))
                .Where(c => c.Descricao.Equals("Teste Descrição Editado"))
                .Where(c => c.ModeloId.Equals(2))
                .Where(c => c.AnoFabricacao.Equals(2022))
                .Where(c => c.AnoModelo.Equals(2022))
                .FirstOrDefault());
        }

        [Fact]
        public async void Test_editar_modelo_invalido()
        {
            //Arrange
            var context = new EwareTesteContext();
            var createModel = new CreateModel(context);
            var editModel = new EditModel(context);

            //Act
            createModel.Caminhao = new();
            createModel.Caminhao.Nome = "Teste Nome";
            createModel.Caminhao.Descricao = "Teste Descrição";
            createModel.Caminhao.ModeloId = 1;
            createModel.Caminhao.AnoFabricacao = 2022;
            createModel.Caminhao.AnoModelo = 2023;
            await createModel.OnPostAsync();
            editModel.Caminhao = createModel.Caminhao;            
            editModel.Caminhao.ModeloId = 3;            
            await editModel.OnPostAsync();

            //Assert
            Assert.NotNull(editModel.ErroValidacaoForm);
        }
    }
}