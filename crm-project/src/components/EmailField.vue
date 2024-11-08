<template>
    <div class="input-group mb-3">
        <input type="text" class="form-control" placeholder="Your Email address" v-model="localEmail"
            @input="validateInput" />
        <div v-if="!isValid" style="color: red;">Incorrect data format</div>
    </div>
</template>

<script>
export default {
    props: ['email'],  // Пропс для email
    data() {
        return {
            localEmail: this.email,  // Створюємо локальну змінну для двостороннього зв'язку
            isValid: true  // Початковий стан перевірки
        };
    },
    watch: {
        // Стежимо за змінами пропса
        email(newEmail) {
            this.localEmail = newEmail; // Оновлюємо локальну змінну, коли змінюється пропс
        },
        localEmail(newLocalEmail) {
            this.$emit('update:email', newLocalEmail); // Випромінюємо оновлене значення email
        }
    },
    methods: {
        validateInput() {
            const regex = /^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,}$/;
            this.isValid = regex.test(this.localEmail);  // Перевіряємо локальну змінну
        },
    },
};
</script>