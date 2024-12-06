<template>
    <div>
        <button @click="OrderTableCurrrentPage = 'main'">Main</button>
        <h1>Orders that you bought:</h1>
        <table>
            <thead>
                <tr>
                    <th>Name</th>
                    <th>Amount</th>
                    <th>Price</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="order in PersonalOrders.filter(row => row.ByuerId == PersonalByuerId)" :key="order.Id">
                    <td><input id="Name" type="text" v-model="order.CultureId" disabled></td>
                    <td><input id="Amount" type="text" v-model="order.OrderAmount" disabled></td>
                    <td><input id="Price" type="text" v-model="order.OrderPrice" disabled></td>
                </tr>
            </tbody>
        </table>
    </div>
</template>

<style scoped>
/* Контейнер для заголовку та кнопки */
div {
    display: flex;
    flex-direction: column;
    align-items: center;
    padding: 20px;
    max-width: 800px;
    margin: 0 auto;
    background-color: #f9f9f9;
    border-radius: 8px;
    box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
}

/* Кнопка */
button {
    background-color: #007bff;
    color: white;
    padding: 10px 20px;
    border: none;
    border-radius: 5px;
    cursor: pointer;
    margin-bottom: 20px;
    transition: background-color 0.3s ease;
}

button:hover {
    background-color: #0056b3;
}

button:active {
    background-color: #004085;
}

/* Стиль заголовка */
h1 {
    text-align: center;
    margin-bottom: 20px;
}

/* Стиль таблиці */
table {
    width: 100%;
    border-collapse: collapse;
}

th, td {
    padding: 10px;
    text-align: left;
    border: 1px solid #ddd;
}

th {
    background-color: #007bff;
    color: white;
}

td input {
    width: 94%;
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
export default {
props: ['localCurrentPage', 'localOrders', 'localByuerId'],
data() {
    return {
        PersonalOrders: this.localOrders,
        OrderTableCurrrentPage: this.localCurrentPage,
        PersonalByuerId: this.localByuerId,
    }
},
watch: {
    localCurrentPage(newlocalCurrentPage) {
            this.OrderTableCurrrentPage = newlocalCurrentPage; // Оновлюємо локальну змінну, коли змінюється пропс
        },
        OrderTableCurrrentPage(newOrderTableCurrrentPage) {
            this.$emit('update:localCurrentPage', newOrderTableCurrrentPage); // Випромінюємо оновлене значення users
        }
    },
}
</script>