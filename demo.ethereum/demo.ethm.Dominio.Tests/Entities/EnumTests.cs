using System;
using System.Collections.Generic;
using System.Text;
using demo.ethm.Dominio.Entities.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace demo.ethm.Dominio.Testes.Entities
{
    [TestClass]
    public class EnumTests
    {
        [TestMethod]
        public void VerifyColorEnumFriendlyValues()
        {
            var EnumsString = EnumMethods.ObterTiposDeUsuario(1);
            Assert.AreEqual("Advogado|Escritor|Escritório|", EnumsString);
        }
    }
}
