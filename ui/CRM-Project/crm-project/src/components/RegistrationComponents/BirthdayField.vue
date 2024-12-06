<template>
    <div class="input-group mb-3">
        <input type="date" class="form-control" v-model="localBirthday"
        @input="validateInput" required />
    <div v-if="!isValid" style="color: red;">Incorrect data format</div>
    </div>
</template>

<script>
export default {
    props: ['birthday'],  // Пропс для дати народження
    data() {
        return {
            localBirthday: this.birthday,  // Локальна змінна для двостороннього зв’язку
            isValid: true
        };
    },
    watch: {
        // Стежимо за змінами пропса
        birthday(newBirthday) {
            this.localBirthday = newBirthday;  // Оновлюємо локальну змінну, коли змінюється пропс
        },
        localBirthday(newLocalBirthday) {
            this.$emit('update:birthday', newLocalBirthday); // Випромінюємо оновлене значення email
        }
    },
    methods: {
        validateInput() {    
            const regex = /^\d{4}-\d{2}-\d{2}$/;
            this.isValid = regex.test(this.localBirthday);
        },
    },
};
</script>
