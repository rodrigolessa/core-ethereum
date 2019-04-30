using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Nethereum.Hex.HexTypes;

namespace demo.ethm.IntegracaoEthereum
{
    public class Transacoes
    {
        public async Task<(string, DateTime)> CriarTransacoesAsync(string hashDoArquivo, string assJwt)
        {
            var senderAddress = "0x3c30BdDA887BeE28e6D09F801E3f6B0E7AE876F6";
            var client = new Nethereum.JsonRpc.Client.RpcClient(new Uri("https://ropsten.infura.io/v3/69d76e2ed4cf459091d6113d9f18b0c0"));
            var account = new Nethereum.Web3.Accounts.Account("5C634792510B7D93283FA4A03D6B396CEECE19E860BF0E0632C54BE513B10259");
            var web3 = new Nethereum.Web3.Web3(account, client);

            var contractAddress = "0xF6676908F5E3580C7bFF689dF739D843607DBc6c";
            var abi = @"[{'constant':false,'inputs':[{'name':'hashArquivo','type':'string'},{'name':'assinatura','type':'string'}],'name':'adicionarAssinatura','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':false,'inputs':[{'name':'hashArquivo','type':'string'}],'name':'removerAssinatura','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':false,'inputs':[],'name':'removeSdaContract','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':true,'inputs':[],'name':'empresa','outputs':[{'name':'','type':'string'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[{'name':'hashArquivo','type':'string'}],'name':'buscarAssinatura','outputs':[{'name':'','type':'string'}],'payable':false,'stateMutability':'view','type':'function'},{'inputs':[{'name':'_empresa','type':'string'}],'payable':false,'stateMutability':'nonpayable','type':'constructor'},{'anonymous':false,'inputs':[{'indexed':true,'name':'hashArquivo','type':'string'},{'indexed':true,'name':'assinatura','type':'string'},{'indexed':false,'name':'datahora','type':'uint256'}],'name':'AssinaturaAdicionada','type':'event'}]";

            var contract = web3.Eth.GetContract(abi, contractAddress);
            var adicionarAssinaturaFunc = contract.GetFunction("adicionarAssinatura");

            try
            {
                var trx = await adicionarAssinaturaFunc.SendTransactionAsync(
                    senderAddress,
                    new HexBigInteger(900000),
                    new HexBigInteger(1000),
                    new HexBigInteger(0),
                    hashDoArquivo,
                    assJwt);

                // TODO: Verificar se já foi processada

                Nethereum.RPC.Eth.DTOs.TransactionReceipt receiptAdd = null;
                while (receiptAdd == null)
                {
                    receiptAdd = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(trx);
                    System.Threading.Thread.Sleep(1000);
                }

                return (trx, DateTime.Now);
            }
            catch (Exception ex)
            {
                return (string.Empty, DateTime.Now);
            }
        }

        public void VerificarTransacoes()
        {

        }
    }
}
