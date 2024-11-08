<template>
    <div class="input-group mb-3">
        <select v-model="localRole" class="custom-select" @change="validateInput" required>
            <option value="" disabled>Choose your role:</option>
            <option value="Buyer">Buyer</option>
            <option value="Seller">Seller</option>
        </select>
    </div>
</template>

<script>
export default {
    props: {
        role: {
            type: String,
            required: true,
        },
    },
    data() {
        return {
            localRole: this.role, // Створюємо локальну змінну для двостороннього зв'язку
        };
    },
    watch: {
        // Стежимо за змінами пропса
        role(newRole) {
            this.localRole = newRole; // Оновлюємо локальну змінну, коли змінюється пропс
        },
        localRole(newLocalRole) {
            this.$emit('update:role', newLocalRole); // Випромінюємо оновлене значення email
        }
    },
    methods: {
        validateInput() {
        },
    },
};
</script>
