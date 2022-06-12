
export class Order {
    orderId: number;
    orderDate: Date = new Date();
    orderNumber: string;
    items: Array<OrderItem> = new Array<OrderItem>();

    get subtotal(): number {
        // use lodash to sum up thte totals
        // _.map() creates a collection of unitPrice * quantity for every 
        // item. then _.sum() is going to add it all.
        return _.sum(_.map(this.items, i => i.unitPrice * i.quantity));
    };
}

export class OrderItem {
    id: number;
    quantity: number;
    unitPrice: number;
    productId: number;
    productCategory: string;
    productSize: string;
    productTitle: string;
    productArtist: string;
    productArtId: string;
}