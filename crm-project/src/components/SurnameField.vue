<template>
    <div class="input-group mb-3">
        <input type="text" class="form-control" placeholder="Your Surname" v-model="localSurname"
            @input="validateInput" required />
        <div v-if="!isValid" style="color: red;">Incorrect data format</div>
    </div>
</template>

<script>
export default {
    props: ['surname'],  // Пропс для прізвища
    data() {
        return {
            localSurname: this.surname,  // Локальна змінна для двостороннього зв’язку
            isValid: true  // Початковий стан перевірки
        };
    },
    watch: {
        // Стежимо за змінами пропса
        surname(newSurname) {
            this.localSurname = newSurname;  // Оновлюємо локальну змінну, коли змінюється пропс
        },
        localSurname(newLocalSurname) {
            this.$emit('update:surname', newLocalSurname); // Випромінюємо оновлене значення email
        }
    },
    methods: {
        validateInput() {
            const regex = /^[a-zA-Z0-9_]{3,}$/;  // Перевірка на допустимі символи: літери, цифри та _
            this.isValid = regex.test(this.localSurname);  // Перевірка локальної змінної
        },
    },
};
</script>
