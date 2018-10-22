/*using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Security;
using UnityEngine.UI;

[System.Serializable]
public class ProductItem
{
    public string namaProduk;
    public string productID;
    public Text labelHarga;
    public GameObject productObject;
}

public class InAppManager : MonoBehaviour, IStoreListener
{

    private const string OFFLINE_DISPLAY = "Offline";

    public List<ProductItem> Product;

    private static IStoreController m_StoreController;
    private static IExtensionProvider m_StoreExtensionProvider;
    private CrossPlatformValidator validator;
    private bool m_IsGooglePlayStoreSelected;

    
    public void BuyItem1()
    {
        BuyProductID(Product[0].productID);
    }
    public void BuyItem2()
    {
        BuyProductID(Product[1].productID);
    }
    public void BuyItem3()
    {
        BuyProductID(Product[2].productID);
    }
        
    public void Start(){
        if (m_StoreController == null)
        {
            initializePurchasing();
        }
    }

    public void initializePurchasing(){
        if (IsInitialized())
        {
            return;
        }
            
        var module = StandardPurchasingModule.Instance();
        module.useFakeStoreUIMode = FakeStoreUIMode.StandardUser;

        m_IsGooglePlayStoreSelected = Application.platform == RuntimePlatform.Android && module.androidStore == AndroidStore.GooglePlay;
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        for(int i = 0; i < Product.Count; i++)
        {
            builder.AddProduct(Product[i].productID, ProductType.Consumable);
        }
       
            
        validator = new CrossPlatformValidator(GooglePlayTangle.Data(), AppleTangle.Data(), Application.identifier);
        UnityPurchasing.Initialize(this, builder);
    }

    private bool IsInitialized()
    {
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }

    void BuyProductID(string productId)
    {

        if (IsInitialized())
        {
            Product product = m_StoreController.products.WithID(productId);

            if (product != null && product.availableToPurchase)
            {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                m_StoreController.InitiatePurchase(product);
            }
            else
            {
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
        }
        else
        {
            Debug.Log("BuyProductID FAIL. Not initialized.");
        }
    }

    public void RestorePurchases()
    {
            
        if (!IsInitialized())
        {
            Debug.Log("RestorePurchases FAIL. Not initialized.");
            return;
        }
            

        if (Application.platform == RuntimePlatform.IPhonePlayer ||
            Application.platform == RuntimePlatform.OSXPlayer)
        {
            Debug.Log("RestorePurchases started ...");
            var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
            apple.RestoreTransactions((result) => {
                Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
            });
        }
        else
        {
            Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
        }
            
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        m_StoreController = controller;
        m_StoreExtensionProvider = extensions;

        if (controller == null)
            return;


        /// Setup Consumable Product Price
        for(int i = 0; i < Product.Count; i++)
        {
            Product p = controller.products.WithID(Product[i].productID);
            if (p != null)
            {
                //ganti label harga nya:
                Product[i].labelHarga.text = p.metadata.localizedPriceString;
            }
        }

        if(GlobalManager.Instance.DATA.SaveData.telahMembeliNoAds == true)
        {
            if (Product[0].productObject != null)
            {
                Product[0].productObject.SetActive(false);
            }
        }
    }

    public void RefreshProductPrice()
    {
        if (m_StoreController == null)
            return;

        /// Setup Consumable Product Price
        for (int i = 0; i < Product.Count; i++)
        {
            Product p = m_StoreController.products.WithID(Product[i].productID);
            if (p != null)
            {
                //ganti label harganya
                Product[i].labelHarga.text = p.metadata.localizedPriceString;
            }
        }
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
        
        for (int i = 0; i < Product.Count; i++)
        {
            Product[i].labelHarga.text = OFFLINE_DISPLAY;
        }
        
    }

    public void OnPurchaseFailed(Product i, PurchaseFailureReason p)
    {
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", i.definition.storeSpecificId, p));
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
    {
         
        bool validPurchase = true;

        if (m_IsGooglePlayStoreSelected ||
        Application.platform == RuntimePlatform.IPhonePlayer ||
        Application.platform == RuntimePlatform.OSXPlayer ||
        Application.platform == RuntimePlatform.tvOS)
        {
            try
            {
                var result = validator.Validate(e.purchasedProduct.receipt);
                Debug.Log("Receipt is valid. Contents:");
                foreach (IPurchaseReceipt productReceipt in result)
                {
                    Debug.Log(productReceipt.productID);
                    Debug.Log(productReceipt.purchaseDate);
                    Debug.Log(productReceipt.transactionID);

                    GooglePlayReceipt google = productReceipt as GooglePlayReceipt;
                    if (null != google)
                    {
                        Debug.Log(google.purchaseState);
                        Debug.Log(google.purchaseToken);
                    }

                    AppleInAppPurchaseReceipt apple = productReceipt as AppleInAppPurchaseReceipt;
                    if (null != apple)
                    {
                        Debug.Log(apple.originalTransactionIdentifier);
                        Debug.Log(apple.subscriptionExpirationDate);
                        Debug.Log(apple.cancellationDate);
                        Debug.Log(apple.quantity);
                    }
                }
            }
            catch (IAPSecurityException)
            {
                Debug.Log("Invalid receipt, not unlocking content");
                validPurchase = false;
                return PurchaseProcessingResult.Complete;
            }
        }
        if (validPurchase)
        {
            if (e.purchasedProduct.definition.id == Product[0].productID)
            {
                // berhasi membeli item 1
                GlobalManager.Instance.DATA.SaveData.telahMembeliNoAds = true;
                Product[0].productObject.SetActive(false); //hide tombol nya
                Debug.Log("Berhasil Membeli ItemNoAds");
            }
            else if (e.purchasedProduct.definition.id == Product[1].productID)
            {
                // berhasi membeli item 2
            }
            else if (e.purchasedProduct.definition.id == Product[2].productID)
            {
                // berhasi membeli item 3
            }
            else
            {
                Debug.Log("No Purchase");
            }

            GlobalManager.Instance.DATA.SaveGameData(); //save gameData
        }
            
        return PurchaseProcessingResult.Complete;
    }
}
*/


