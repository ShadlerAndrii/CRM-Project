<template>
    <BuyerOrderTable v-if="localCurrentPage == 'BuyerOrder'" v-model:localCurrentPage="localCurrentPage"
        v-model:localOrders="localOrders" v-model:localByuerId="localByuerId" />
    <div v-if="localCurrentPage == 'main'">
        <button @click="localCurrentPage = 'BuyerOrder'">Profile</button>
        <h1>All orders:</h1>
        <div class="table-container" v-if="localCurrentPage == 'main'">
            <table>
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>Amount</th>
                        <th>Price</th>
                        <th>Buy</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="order in localOrders" :key="order.Id">
                        <td><input id="Name" type="text" v-model="order.CultureId" disabled></td>
                        <td><input id="Amount" type="text" v-model="order.OrderAmount" disabled></td>
                        <td><input id="Price" type="text" v-model="order.OrderPrice" disabled></td>
                        <td>
                            <button id="ByuerId"
                                @click="changeSellersOrders(order.Id, order.SellerId, order.CultureId, order.OrderAmount, order.OrderPrice);"
                                v-if="order.ByuerId == null">Order</button>
                            <button id="ByuerId" v-if="order.ByuerId != null" disabled>Bought</button>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</template>

<style scoped>

/* Основна контейнерна стилізація */
.table-container {
    padding: 20px;
    max-width: 900px;
    margin: 20px auto;
    background-color: #ffffff;
    border-radius: 10px;
    box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
    border: 1px solid #e0e0e0;
}

/* Кнопка для переходу до профілю */
button {
    padding: 10px 15px;
    font-size: 14px;
    border-radius: 5px;
    border: none;
    background-color: #007bff;
    color: white;
    cursor: pointer;
    transition: background-color 0.3s ease;
    margin-bottom: 20px;
}

button:hover {
    background-color: #0056b3;
}

button:disabled {
    background-color: #cccccc;
    cursor: not-allowed;
}

/* Заголовок */
h1 {
    text-align: center;
    font-size: 1.8rem;
    margin-bottom: 20px;
    color: #333333;
}

/* Таблиця */
table {
    width: 100%;
    border-collapse: collapse;
}

table th,
table td {
    text-align: left;
    padding: 10px;
    border: 1px solid #e0e0e0;
}

table th {
    background-color: #f5f5f5;
    font-weight: bold;
    text-transform: uppercase;
    font-size: 14px;
}

table tbody tr:nth-child(even) {
    background-color: #f9f9f9;
}

table tbody tr:hover {
    background-color: #f1f1f1;
}

/* Вхідні дані */
input[type="text"] {
    padding: 8px;
    width: 94%;
    border: 1px solid #cccccc;
    border-radius: 5px;
    font-size: 14px;
    background-color: #f9f9f9;
}

input[type="text"]:focus {
    outline: none;
    border-color: #007bff;
    background-color: #ffffff;
}

/* Кнопки в таблиці */
table button {
    padding: 5px 10px;
    margin: auto;
    font-size: 12px;
    border-radius: 5px;
    border: none;
    background-color: #28a745;
    color: white;
    cursor: pointer;
    transition: background-color 0.3s ease;
}

table button:hover {
    background-color: #218838;
}

table button:disabled {
    background-color: #6c757d;
    cursor: not-allowed;
}
</style>


<script>
import BuyerOrderTable from './BuyerOrderTable.vue';

import ordersAPI from '../../ordersAPI.js';
export default {
    props: ['order', 'MainUserId', 'currentPage', 'MainToken'],
    components: {
        BuyerOrderTable,
    },
    data() {
        return {
            localOrders: this.order,
            localByuerId: this.MainUserId,
            localCurrentPage: this.currentPage,
            localToken: this.MainToken,
            cultureDictionary: {
                1: 'Grain',
                2: 'Legume',
                3: 'Oilseed',
                4: 'Root',
                5: 'Fruit',
                6: 'Vegetable',
                7: 'Green',
                8: 'Berry',
                9: 'Technical',
                10: 'Fodder',
                11: 'Beverage',
                12: 'Spice'
            }
        };
    },
    methods: {
        async getSellersOrders() {
            this.localOrders = (await ordersAPI.refreshData(this.localToken)).slice();                        
            this.convertCulture();
        },
        async changeSellersOrders(Id, SellerId, CultureId, OrderAmount, OrderPrice) {
            let revertedCultureId = await this.reverseCulture(CultureId);
            console.log(revertedCultureId);

            await ordersAPI.changeOrder(Id, this.localByuerId, SellerId, revertedCultureId, OrderAmount, OrderPrice, this.localToken);
            this.getSellersOrders();
        },
        async convertCulture() {
            for (let i = 0; i < this.localOrders.length; i++) {
                const order = this.localOrders[i];
                // Заміна CultureId на текстове значення
                const cultureName = this.cultureDictionary[order.CultureId];
                order.CultureId = cultureName; // Додаємо нову властивість
            }
        },
        async reverseCulture(CultureName) {
            return Object.keys(this.cultureDictionary).find(key => this.cultureDictionary[key] === CultureName);
        }
    },
    mounted: function () {
        this.getSellersOrders();
    }
}
</script>