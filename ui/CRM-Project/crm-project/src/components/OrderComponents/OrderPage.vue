<template>
    <button @click="localCurrentPage = 'main'">Main</button>
    <OrderForm v-model:orderData="orderData" />
</template>

<script>
import OrderForm from './OrderForm.vue';

export default {
    props: ['currentPage'],
    components: {
        OrderForm,
    },
    data() {
        return {
            localCurrentPage: '',
            quantity: ''
        };
    },
    watch: {
        currentPage(newCurrentPage) {
            this.localCurrentPage = newCurrentPage; // Оновлюємо локальну змінну, коли змінюється пропс
        },
        localCurrentPage(newLocalCurrentPage) {
            this.$emit('update:currentPage', newLocalCurrentPage); // Випромінюємо оновлене значення users
        }
    },
};
</script>