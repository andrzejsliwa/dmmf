namespace Ordering

// Product code info

type ProductCode =
    | Widget of WidgetCode
    | Gizmo of GizmoCode

type OrderQuantity =
    | Unit of UnitQuantity
    | Kilogram of KilogramQuantity

module DemoIt =
    let valid = WidgetCode.create "W1234"
    let invalid = WidgetCode.create "wat"
    
    let unwrapped =
        match valid with
        | Ok(code) -> WidgetCode.value code
        | Error(_) -> ""

    
// Helper -- we'll replace this later
type Undefined = exn

type OrderId = Undefined
type OrderLineId = Undefined
type CustomerId = CustomerId of int

type CustomerInfo = Undefined
type ShippingAddress = Undefined
type BillingAddress = Undefined
type Price = Undefined
type BillingAmount = Undefined

type Order = {
    Id : OrderId
    CustomerInfo : CustomerInfo
    ShippingAddress : ShippingAddress
    BillingAddress : BillingAddress
    OrderLines : OrderLine list
    AmountToBill: BillingAmount
    }

and OrderLine = {
    Id : OrderLineId
    OrderId : OrderId
    ProductCode : ProductCode
    OrderQuantity : OrderQuantity
    Price : Price
    }


type AcknowledgementSent = Undefined
type OrderPlaced = Undefined
type BillableOrderPlaced = Undefined

type PlaceOrderEvents = {
    AcknowledgementSent : AcknowledgementSent
    OrderPlaced : OrderPlaced
    BillableOrderPlaced : BillableOrderPlaced
    }

type PlaceOrderError =
    | ValidationError of ValidationError list

and ValidationError = {
    FieldName : string
    ErrorDescription : string
    }

type UnvalidatedOrderLine = {
    ProductCode : string
    OrderQuantity : int
    }

type UnvalidatedOrder = {
    OrderId : string
    CustomerInfo : string // maybe?
    ShippingAddress : string
    OrderLines : UnvalidatedOrderLine list
    }

type ValidatedOrder = Undefined

type ValidationResponse<'a> = Async<Result<'a,ValidationError list>>

type ValidateOrder =
    UnvalidatedOrder -> ValidationResponse<ValidatedOrder>


type PlaceOrder = UnvalidatedOrder -> Result<PlaceOrderEvents,PlaceOrderError>


type QuoteForm = Undefined
type OrderForm = Undefined

type CategorizedMail =
    | Quote of QuoteForm
    | Order of OrderForm


type ProductCatalog = Undefined
type PricedOrder = Undefined

type CalculatePrices = OrderForm -> ProductCatalog -> PricedOrder

type InvoiceId = InvoiceId of int

type UnpaidInvoice = {
    InvoiceId : InvoiceId
}

type PaidInvoice = {
    InvoiceId : InvoiceId
}

type Invoice =
    | Paid of PaidInvoice
    | Unpaid of UnpaidInvoice
