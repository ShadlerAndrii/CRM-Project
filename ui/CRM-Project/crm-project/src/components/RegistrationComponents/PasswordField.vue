<template>
    <div class="input-group mb-3">
        <input type="text" class="form-control" placeholder="Your Password" v-model="localPassword"
            @input="validateInput" />
        <div v-if="!isValid" style="color: red;">Incorrect data format</div>
    </div>
</template>

<script>
export default {
    props: ['password'],
    data() {
        return {
            localPassword: this.password,
            isValid: true
        };
    },
    watch: {
        // Стежимо за змінами пропса
        password(newPassword) {
            this.localPassword = newPassword;  // Оновлюємо локальну змінну, коли змінюється пропс
        },
        localPassword(newLocalPassword) {
            this.$emit('update:password', newLocalPassword); // Випромінюємо оновлене значення email
        }
    },
    methods: {
        validateInput() {
            const regex = /^[A-Za-z0-9\W+]{6,}$/;
            this.isValid = regex.test(this.localPassword);
        },
    },
};
</script>