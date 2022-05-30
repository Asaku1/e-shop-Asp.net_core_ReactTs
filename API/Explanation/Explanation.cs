using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Explanation
{
    public class Explanation
    {
        ////***NOTE: To do { Link } which is navigation in React, ...
        ////         see "ProductCard.tsx", "BasketPage.tsx", & "CheckoutPage.tsx"...
        ////         and App.tsx
        ////
        //// Lesson 57: Loading Indicator

        //// ***NOTE: "App.tsx" main purpose is to have the navigation Links.
        ////           Therefore, place all navigation Links inside "App.tsx"
        //// 
        ////  C:\Users\Ayogh\Desktop\Asp.net_Core_React\client>
        ////
        ////Type in google: nuget dotnet-ef 5.0.8
        ////Click on "dotnet-ef 5.0.8 - NuGet Gallery"
        ////Copy: dotnet tool install --global dotnet-ef --version 5.0.8
        ////Paste it inside Asp.net_Core_React/API: dotnet tool install --global dotnet-ef --version 5.0.8
        ////Migrations command: dotnet ef migrations add InitialCreate -o Data/Migrations
        ////
        ////See Lessons 14 and 15 for seeding the database table
        ////
        ////Add CORS: Lesson 26
        ////
        ////Lesson 28: Adding a Typescript interface for the product; Type in Browser: json to ts
        ////
        ////Lesson 31: Material UI: *** We chose version 5.6.0
        //// To use Material UI:
        ////  a)-On the LHS click on "Customization" or click on "Component API"
        ////  1-Click on "Typography"
        ////  2-We searched for <ThemeProvider theme={theme}>...</ThemeProvider>
        ////  b)-Click on "Components"
        ////  1-Inputs: like Buttons and Text Fields
        ////  2-Data Dispaly: like List and Tables and Icons
        ////  3-Surfaces: like App Bar and Cards
        ////  -**There are many others
        //// 
        ////  NOTE: In the Folders "public/images/products", we get the .png pictures we used. Notice the folder "products", it conforms with
        ////        Notice, it conforms with:
        ////                interface Props {
        ////                    products: Product[]; //"products"
        ////                    addProduct: () => void; 
        ////                }
        //// Lesson 41: Adding "BrowserRouter": It is for making it possible to route components/pages.
        //// Lesson 42: Adding "Nav Link": We need "Nav Link" so that instead of 
        ////                               having to perform routing by typing the
        ////                               component name in the browser, it 
        ////                               provides the ability for us to click on
        ////                               the component we want to go to.
        ////            -Click on "Component API"
        ////            -Here you find "List" and "ListItem"
        ////
        ////
        //// Lesson 44: I am interested in ProductCard.tsx on how the "product.id" was created.
        ////<List>
        ////   <ListItem component = { NavLink } to={`/catalog/${product.id}`}>
        ////                       View1
        ////   </ListItem>
        ////</List>
        ////
        //// 
        //// Likewise, another way of accomplishing navigation is shown below:
        ////<Button component={Link} to={`/catalog/${product.id}`} size="small">
        ////             View
        ////</Button>
        ////
        //// ***NOTE: Imports should be: import { Link, NavLink } from "react-router-dom";
        ////
        //// Lesson 51: How to test the errors we modeled. We shall use the AboutPage.tsx
        ////
        //// Lesson 53: @time=5:36, see how to override typescript by using the exclamation sign, !
        ////
        ////
        //// Lesson 62: 
        ////           public List<BasketItem> Items { get; set; } = new List<BasketItem>();
        ////           "Items" has "BasketItem" properties: which are "Id", "Quantity", "ProductId", & "Product".
        ////            *** Items: {Id, Quantity, ProductId, Product}, i.e., Items.Id, Items.Quantity, Items.ProductId, Items.Product
        ////
        ////           public void AddItem(Product product, int quantity) // "product" in "(Product product)" has all the properties of "Product.cs"
        ////           *** product: {Id, Name, Description, Price, PictureUrl, Type, Brand, QuantityInStock}
        ////
        //// Lesson 72: This section does design for the "Table" for "BasketPage.tsx".
        ////
        //// Lesson 81: See how "onChange" event is used for input text fields.
        ////
        ////    //***NOTE: "onChange" is the event used for inputting data into a form. 
        ////               Both code below are done in vscode.
        ////
        ////        function handleInputChange(event: any) {
        ////            setQuantity(event.target.value);
        ////        }
        ////
        ////    //Below is the React material ui input Text Form.
        ////    <TextField
        ////       onChange={handleInputChange}
        ////       variant="outlined"
        ////       type="number"
        ////       label="Quantity in Cart"
        ////       fullWidth
        ////       value = { quantity }
        ////    />
        ////
        ////
        ////
        //// Lesson 84: Redux Explanation
        //// Lesson 85: Installing and using Redux
        ////            Website: https://redux.js.org
        ////            C:\Users\Ayogh\Desktop\Asp.net_Core_React\client>npm install redux react-redux
        ////
        ////
        ////
        ////

    }
}