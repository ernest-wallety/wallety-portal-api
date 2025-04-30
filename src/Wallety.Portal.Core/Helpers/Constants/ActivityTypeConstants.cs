namespace Wallety.Portal.Core.Helpers.Constants
{
    public static class ActivityTypeConstants
    {
        public static readonly Guid LOGIN = Guid.Parse("37c79fcd-f461-400c-9f67-fa6e4e638c76");
        public static readonly Guid PURCHASE = Guid.Parse("4e1f6a3e-de05-45cf-8e93-c68939c57d6a");
        public static readonly Guid TRANSFER = Guid.Parse("1d56783e-3c46-4c7b-b3fa-d9ffd6402c09");
        public static readonly Guid LOGOUT = Guid.Parse("22158164-2bc6-472f-9d1e-3d9be53e6ca1");
        public static readonly Guid TOP_UP_REQUEST = Guid.Parse("312fc18e-aed2-4c0e-b251-d2d7b62ee56b");
        public static readonly Guid TOP_UP = Guid.Parse("6e9ffcfd-0abe-4e51-a4de-33d94c4b1417");
        public static readonly Guid PAGE_LOAD = Guid.Parse("fec612a4-1b15-4a11-90a5-4276d92853bb");
        public static readonly Guid PAY_ME_REQUEST = Guid.Parse("5e9d485e-f1b4-47e0-bd51-d48de4e303c7");
        public static readonly Guid PAY_FOR_ME_REQUEST = Guid.Parse("c3ebd026-ca74-442c-834b-3251313e3b33");
        public static readonly Guid TOP_ME_UP_REQUEST = Guid.Parse("d2b882c1-fd01-4345-a5af-c1728c9b0eb8");
        public static readonly Guid PURCHASE_PAYMENT_REQUEST = Guid.Parse("62dc27c5-70a3-489b-9f8c-028f219a658b");
        public static readonly Guid PAY_FOR_ME_PAYMENT = Guid.Parse("0918778d-7008-4c31-81d1-da9bab1776d6");
        public static readonly Guid PAY_ME_PAYMENT = Guid.Parse("2e118059-7863-46aa-9368-7bd9317899bc");
        public static readonly Guid TOP_ME_UP_PAYMENT = Guid.Parse("91a280f3-b1df-49d3-bd72-f9886b4cde0c");
        public static readonly Guid GET_UNVERIFIED_CUSTOMER_ACCOUNTS = Guid.Parse("00a75e81-0a50-42d6-80ac-430529dc1b29");
        public static readonly Guid LOAD_LOGIN_PAGE = Guid.Parse("16fdc12a-b2c3-4171-9896-92af8ca063d1");
        public static readonly Guid AUTO_LOGOUT = Guid.Parse("8f498ce9-a7e7-45e5-a0aa-91e78e23d395");
        public static readonly Guid FAILED_DEPENDENCY_LOGOUT = Guid.Parse("62c2ed28-2709-4dc4-997d-e1fb99c74489");
        public static readonly Guid LOAD_PURCHASE_PAGE = Guid.Parse("9f3b44ea-de7b-416b-a409-0c857b52dbc5");
        public static readonly Guid POST_PURCHASE_DETAILS = Guid.Parse("842c5666-77ba-4e5a-9dd5-d7eceec10a66");
        public static readonly Guid POST_WALLET_AUTHORIZATION_PIN = Guid.Parse("147476cf-de09-4b9f-8a5d-6301f0709661");
        public static readonly Guid MAKE_WALLET_PAYMENT = Guid.Parse("be05d80f-8cf9-422a-a839-5e9b1f0a06e8");
        public static readonly Guid GET_SERVICE_FEE = Guid.Parse("32276aef-eb0a-4e4c-93a0-5afc6f0a96c5");
        public static readonly Guid LOAD_TOP_UP_PAGE = Guid.Parse("06d1dbe8-0942-4918-a48c-1762ab6dacd5");
        public static readonly Guid POST_TOP_UP_DETAILS = Guid.Parse("83d1d6be-5bae-42e4-8819-206a76a1f32c");
        public static readonly Guid LOAD_PAY_FOR_ME_PAGE = Guid.Parse("c7764a2a-00b1-45c7-bf84-3ca7ecb69bd8");
        public static readonly Guid POST_PAY_FOR_ME_DETAILS = Guid.Parse("3538d967-2d25-4611-96d6-91a96f695056");
        public static readonly Guid LOAD_TOP_ME_UP_PAGE = Guid.Parse("6d4f4490-3b9a-4ca6-bf3f-bbe2d9b2cf55");
        public static readonly Guid LOAD_PLEASE_PAY_ME_PAGE = Guid.Parse("7a4d09f5-6c78-4430-a7a9-1af5916f677b");
        public static readonly Guid POST_TOP_ME_UP_DETAILS = Guid.Parse("5a8a3511-a5e6-43cc-8793-432a49d73d68");
        public static readonly Guid POST_PAY_ME_DETAILS = Guid.Parse("4b379cd2-ca1b-42cc-a61f-2d45f2701059");
        public static readonly Guid LOAD_TRANSFER_PAGE = Guid.Parse("e2248b85-020b-48bc-90ec-d3f822a58303");
        public static readonly Guid POST_TRANSFER_DETAILS = Guid.Parse("37a1fa15-9ae8-4574-94fb-ea5920b00fa3");
    }
}
