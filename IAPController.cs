using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAPController : MonoBehaviour,IStoreListener
{
    IStoreController controller;
    public string[] product;
    public player player;
    private void Awake()
    {
        
    }

    private void Start()
    {
        IAPStart();
    }

    private void IAPStart()
    {
        var module = StandardPurchasingModule.Instance();
        ConfigurationBuilder builder = ConfigurationBuilder.Instance(module);
        foreach (string item in product)
        {
            builder.AddProduct(item, ProductType.Consumable);
        }
        UnityPurchasing.Initialize(this, builder);
    }
    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        this.controller = controller;
    }
    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("Error" + error.ToString());
    }
    public void OnPurchaseFailed(Product i, PurchaseFailureReason p)
    {
        Debug.Log("Error while buying " + p.ToString());
    }
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
    {
        if(string.Equals(e.purchasedProduct.definition.id, product[0], System.StringComparison.Ordinal)){
            player.ReklamKaldirSatinAlindi();
            return PurchaseProcessingResult.Complete;
        }
        else{
            return PurchaseProcessingResult.Pending;
        }
    }

    public void IAPButton(string id)
    {
        Product proc = controller.products.WithID(id);
        if(proc != null && proc.availableToPurchase)
        {
            Debug.Log("Buying");
            controller.InitiatePurchase(proc);
        }
        else
        {
            Debug.Log("Not");
        }
    }
}
