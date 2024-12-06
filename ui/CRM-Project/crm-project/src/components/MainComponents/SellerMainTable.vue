<template>
    <SellerOrderTable @delete="deleteSellersOrders" @refresh="getSellersOrders"
        v-if="localCurrentPage == 'SellerOrder'" v-model:localCurrentPage="localCurrentPage"
        v-model:localOrders="localOrders" v-model:localSellerId="localSellerId" v-model:localToken="localToken" />
    <FieldPage v-if="localCurrentPage == 'SellerField'" v-model:localCurrentPage="localCurrentPage"
        v-model:localSellerId="localSellerId" v-model:localToken="localToken" />
    <CulturePage v-if="localCurrentPage == 'SellerCulture'" v-model:localCurrentPage="localCurrentPage"
        v-model:localSellerId="localSellerId" v-model:localToken="localToken" />
    <EquipmentPage v-if="localCurrentPage == 'SellerEquipment'" v-model:localCurrentPage="localCurrentPage"
        v-model:localSellerId="localSellerId" v-model:localToken="localToken" />

    <div class="table-container" v-if="localCurrentPage == 'main'">
        <div class="button-container">
            <button @click="localCurrentPage = 'SellerOrder'">Order</button>
            <button @click="localCurrentPage = 'SellerField'">Field</button>
            <button @click="localCurrentPage = 'SellerCulture'">Culture</button>
            <button @click="localCurrentPage = 'SellerEquipment'">Equipment</button>
        </div>
        <select v-model="CultureId">
            <option value="" disabled>Choose culture name</option>
            <option value="0">Grain</option>
            <option value="1">Legume</option>
            <option value="2">Oilseed</option>
            <option value="3">Root</option>
            <option value="4">Fruit</option>
            <option value="5">Vegetable</option>
            <option value="6">Green</option>
            <option value="7">Berry</option>
            <option value="8">Technical</option>
            <option value="9">Fodder</option>
            <option value="10">Beverage</option>
            <option value="11">Spice</option>
        </select>
        <input type="text" placeholder="Amount of culture" v-model="OrderAmount" @input="validateAmountPrice('OrderAmount')" />
        <input type="text" placeholder="Price of culture" v-model="OrderPrice" @input="validateAmountPrice('OrderPrice')" />
        <button @click="createSellersOrders()" :disabled="!isFormValid">Create Order</button>

        <table>
            <thead>
                <tr>
                    <th>Name</th>
                    <th>Amount</th>
                    <th>Price</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="order in localOrders" :key="order.Id">
                    <td><input id="Name" type="text" v-model="order.CultureId" disabled></td>
                    <td><input id="Amount" type="text" v-model="order.OrderAmount" disabled></td>
                    <td><input id="Price" type="text" v-model="order.OrderPrice" disabled></td>
                </tr>
            </tbody>
        </table>
    </div>
</template>

<style scoped>
.button-container {
    display: flex;
    flex-direction: row;    
    justify-content: space-between;  /* Кнопки будуть рівномірно розподілені по рядку */
    gap: 15px;  /* Відступ між кнопками */
    margin-bottom: 20px;
    width: 100%; /* Розтягує контейнер на всю ширину */
    max-width: 600px; /* Максимальна ширина контейнера */
    margin: 5 auto; /* Центрує контейнер на сторінці */
}

button:hover {
    background-color: #0056b3;
}

button:active {
    background-color: #004085;
}

/* Контейнер для всього вмісту */
div {
    display: flex;
    flex-direction: column;
    align-items: center;
    padding: 20px;
    max-width: 1000px;
    margin: 0 auto;
    background-color: #f9f9f9;
    border-radius: 8px;
    box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
}

/* Блок кнопок для навігації між сторінками */
div>div {
    display: flex;
    justify-content: space-around;
    margin-bottom: 20px;
    width: 100%;
}

/* Стиль для кнопок */
button {
    background-color: #007bff;
    color: white;
    padding: 10px 20px;
    border: none;
    border-radius: 5px;
    cursor: pointer;
    transition: background-color 0.3s ease;
    flex-grow: 1; /* Розтягує кнопки, щоб вони мали рівну ширину */
}

button:hover {
    background-color: #0056b3;
}

button:active {
    background-color: #004085;
}

/* Стиль для селекту та інпутів */
select,
input {
    margin-bottom: 15px;
    padding: 10px;
    font-size: 14px;
    border: 1px solid #ddd;
    border-radius: 5px;
    width: 100%;
    max-width: 300px;
}

/* Стиль таблиці */
table {
    width: 100%;
    border-collapse: collapse;
    margin-top: 20px;
}

th,
td {
    padding: 10px;
    text-align: left;
    border: 1px solid #ddd;
}

th {
    background-color: #007bff;
    color: white;
}

td input {
    width: 95%;
    padding: 8px;
    font-size: 14px;
    border: 1px solid #ddd;
    border-radius: 5px;
    background-color: #f0f0f0;
}

td input:disabled {
    background-color: #e9ecef;
    cursor: not-allowed;
}
</style>


<script>
import SellerOrderTable from './SellerOrderTable.vue';
import FieldPage from '..//FieldComponents/FieldPage.vue';
import CulturePage from '../CultureComponents/CulturePage.vue'
import EquipmentPage from '..//EquipmentComponents/EquipmentPage.vue';

import ordersAPI from '../../ordersAPI.js';
export default {
    props: ['order', 'MainUserId', 'currentPage', 'MainToken'],
    components: {
        SellerOrderTable,
        FieldPage,
        CulturePage,
        EquipmentPage
    },
    data() {
        return {
            localOrders: this.order,
            localSellerId: this.MainUserId,
            localCurrentPage: this.currentPage,
            CultureId: '',
            OrderAmount: '',
            OrderPrice: '',
            localToken: this.MainToken,
            isFormValid: false,
            cultureDictionary: {
                0: 'Grain',
                1: 'Legume',
                2: 'Oilseed',
                3: 'Root',
                4: 'Fruit',
                5: 'Vegetable',
                6: 'Green',
                7: 'Berry',
                8: 'Technical',
                9: 'Fodder',
                10: 'Beverage',
                11: 'Spice'
            }
        };
    },
    methods: {
        async getSellersOrders() {
            this.localOrders = (await ordersAPI.refreshData(this.localToken)).slice();
            this.convertCulture();
        },
        async createSellersOrders() {
            await ordersAPI.createOrder(null, this.localSellerId, this.CultureId, this.OrderAmount, this.OrderPrice, this.localToken);
            this.resetValues();
            await this.getSellersOrders();
        },
        async deleteSellersOrders(Id) {
            await ordersAPI.deleteOrder(Id, this.localToken);
            await this.getSellersOrders();
        },
        async changeSellersOrders(object) {
            
            let revertedCultureId = await this.reverseCulture(object.CultureId);            

            await ordersAPI.changeOrder(object.Id, object.BuyerId, this.localSellerId, revertedCultureId, object.OrderAmount, object.OrderPrice, this.localToken);
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
        },
        async resetValues() {
            this.CultureId = '';
            this.OrderAmount = '';
            this.OrderPrice = '';
        },
        // Валідація для Amount та Price
        async validateAmountPrice(field) {
            const value = this[field];
            // Перевірка на допустимі значення (тільки числа та десятинні розряди)
            const validValue = value.replace(/[^0-9]/g, ''); 
            if (validValue != value) {
                this[field] = validValue; // Якщо є недопустимі символи, видаляємо їх
            }
            await this.checkFormValidity(); // Перевіряємо, чи є правильні значення у полях
        },
        // Перевірка валідності форми
        async checkFormValidity() {
            // Перевіряємо, чи обидва поля Amount та Price є числами
            const isAmountValid = !isNaN(this.OrderAmount) && this.OrderAmount > 0;
            const isPriceValid = !isNaN(this.OrderPrice) && this.OrderPrice > 0;
            this.isFormValid = isAmountValid && isPriceValid; // Якщо обидва поля валідні, активуємо кнопку
        }
    },
    mounted: function () {
        this.getSellersOrders();
    }
}
</script>