<template>
    <div>
        <button @click="OrderTableCurrrentPage = 'main'">Main</button>
        <h1>Orders that you created:</h1>
        <table>
            <thead>
                <tr>
                    <th>Name</th>
                    <th>Amount</th>
                    <th>Price</th>
                    <th>Change/Delete</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="order in PersonalOrders.filter(row => row.SellerId == PersonalSellerId)" :key="order.Id"
                    :id="order.Id">
                    <td><input id="Name" type="text" v-model="order.CultureId" disabled></td>
                    <td><input id="Amount" type="text" v-model="order.OrderAmount" disabled></td>
                    <td><input id="Price" type="text" v-model="order.OrderPrice" disabled></td>
                    <td v-if="order.ByuerId == null">
                        <button @click="changeSellersOrders(order.Id)" v-if="order.Id != editingId">Change</button>
                        <button
                            @click="saveSellersOrders(order.Id, order.ByuerId, order.CultureId, order.OrderAmount, order.OrderPrice)"
                            v-if="order.Id == editingId">Save</button>
                        <button @click="deleteSellersOrders(order.Id)">Delete</button>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</template>

<style scoped>
/* Контейнер для кнопки "Main" */
div {
    text-align: center;
    margin-bottom: 20px;
}

/* Заголовок */
h1 {
    text-align: center;
}

/* Таблиця */
table {
    width: 100%;
    border-collapse: collapse;
    margin: 20px 0;
}

th,
td {
    padding: 10px;
    text-align: center;
    border: 1px solid #ddd;
}

th {
    background-color: #f4f4f4;
}

/* Кнопки в ряд */
button {
    background-color: #007bff;
    color: white;
    padding: 8px 16px;
    border: none;
    border-radius: 5px;
    cursor: pointer;
    transition: background-color 0.3s ease;
    margin: 5px;
}

button:hover {
    background-color: #0056b3;
}

button:active {
    background-color: #004085;
}

/* Для кнопки 'Change' та 'Save' */
button:disabled {
    background-color: #ccc;
    cursor: not-allowed;
}

/* Стиль для рядка таблиці, де можна змінити замовлення */
td button {
    margin-left: 5px;
}

/* Розмітка для вводу */
input {
    width: 97%;
    padding: 5px;
    text-align: center;
    border-radius: 5px;
    border: 1px solid #ddd;
    background-color: #f9f9f9;
}

input:disabled {
    background-color: #e9ecef;
}

/* Вирівнювання кнопок */
button {
    display: inline-block;
}
</style>

<script>
import ordersAPI from '@/ordersAPI';
export default {
    props: ['localCurrentPage', 'localOrders', 'localSellerId', 'localToken'],
    data() {
        return {
            PersonalOrders: this.localOrders,
            OrderTableCurrrentPage: this.localCurrentPage,
            PersonalSellerId: this.localSellerId,
            isEdit: false,
            editingId: '',
            usersToken: this.localToken,
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
        }
    },
    watch: {
        localCurrentPage(newlocalCurrentPage) {
            this.OrderTableCurrrentPage = newlocalCurrentPage; // Оновлюємо локальну змінну, коли змінюється пропс
        },
        OrderTableCurrrentPage(newOrderTableCurrrentPage) {
            this.$emit('update:localCurrentPage', newOrderTableCurrrentPage); // Випромінюємо оновлене значення users
        },
        localOrders(newOrders) {
            this.PersonalOrders = [...newOrders]; // Оновлюємо локальні дані, якщо батьківські змінилися
        }
    },
    methods: {
        async deleteSellersOrders(Id) {
            this.$emit('delete', Id)
        },
        async changeSellersOrders(Id) {
            if (this.isEdit) {
                alert('Save first')
            }
            else {
                let inputs = await document.getElementById(Id).getElementsByTagName('input');
                inputs = Array.from(inputs).filter(el => el.getAttribute('id') !== 'Name');
                inputs.forEach(el => el.disabled = false);

                this.isEdit = true;
                this.editingId = Id;
            }

        },
        async saveSellersOrders(Id, ByuerId, CultureName, OrderAmount, OrderPrice) {
            if (!this.validateOrder(OrderAmount, OrderPrice)) {
                alert('Amount and Price must be valid numbers only!');
                return; // Якщо валідація не пройшла, не відправляємо дані на бек
            }

            let inputs = await document.getElementById(Id).getElementsByTagName('input');
            inputs = Array.from(inputs).filter(el => el.getAttribute('id') !== 'Name');
            inputs.forEach(el => el.disabled = true);

            let CultureId = await this.reverseCulture(CultureName);

            this.isEdit = false;
            this.editingId = '';

            await ordersAPI.changeOrder(Id, ByuerId, this.PersonalSellerId, CultureId, OrderAmount, OrderPrice, this.usersToken);
        },
        async reverseCulture(CultureName) {
            return Object.keys(this.cultureDictionary).find(key => this.cultureDictionary[key] === CultureName);
        },
        // Метод для валідації Amount та Price
        async validateOrder(OrderAmount, OrderPrice) {
            const amountPattern = /^[0-9]+(\.[0-9]+)?$/;  // Перевірка, чи є числом (з можливими десятковими знаками)
            const pricePattern = /^[0-9]+(\.[0-9]+)?$/;   // Аналогічно для ціни

            return amountPattern.test(OrderAmount) && pricePattern.test(OrderPrice);
        }
    },
    mounted: function () {
        this.$emit('refresh');
    }
}
</script>