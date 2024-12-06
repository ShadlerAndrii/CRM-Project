<template>
    <div class="input-group mb-3">
        <input type="text" class="form-control" placeholder="Your Password" v-model="localPassword" />
    </div>
</template>

<script>
export default {
    props: ['password'],
    data() {
        return {
            localPassword: this.password
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
    },
};
</script>