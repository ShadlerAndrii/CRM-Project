<template>
    <div class="input-group mb-3">
        <input type="text" class="form-control" placeholder="Your Email address" v-model="localEmail" />
    </div>
</template>

<script>
export default {
    props: ['email'],
    data() {
        return {
            localEmail: this.email,  // Створюємо локальну змінну для двостороннього зв'язку
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
    },
};
</script>