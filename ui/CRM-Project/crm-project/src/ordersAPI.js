import axios from 'axios';

const API_URL = "https://localhost:7135/OrderData";
const API_URL_UD = "https://localhost:7135/OrderData/";
export default {
    async refreshData(token) {
        let data = [];
        await axios.get(API_URL, {
            headers: {
                'Authorization': `Bearer ${token}`
            }
        }).then(
            (response) => {
                data=response.data;
            }
        );
        return data;
    },
    async createOrder(ByuerId, SellerId, CultureId, OrderAmount, OrderPrice, token) {
        const data = {
            "byuerId": ByuerId,
            "sellerId": SellerId,
            "cultureId": CultureId,
            "orderAmount": OrderAmount,
            "orderPrice": OrderPrice
        };

        await axios.post(API_URL, data, {
            headers: {
                'Authorization': `Bearer ${token}`
            }
        });
    },
    async changeOrder(Id, ByuerId, SellerId, CultureId, OrderAmount, OrderPrice, token) {        
        const data = {
            "byuerId": ByuerId,
            "sellerId": SellerId,
            "cultureId": CultureId,
            "orderAmount": OrderAmount,
            "orderPrice": OrderPrice
        };

        await axios.put(API_URL_UD + Id, data, {
            headers: {
                'Authorization': `Bearer ${token}`
            }
        });
    },
    async deleteOrder(Id, token) {
        await axios.delete(API_URL_UD + Id, {
            headers: {
                'Authorization': `Bearer ${token}`
            }
        });
    }
}