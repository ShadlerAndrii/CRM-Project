<template>
    <div class="input-group mb-3">
        <input type="text" class="form-control" placeholder="Your Name" v-model="localName"
            @input="validateInput" required />
        <div v-if="!isValid" style="color: red;">Incorrect data format</div>
    </div>
</template>

<script>
export default {
    props: ['name'],  // Пропс для імені
    data() {
        return {
            localName: this.name,  // Локальна змінна для двостороннього зв’язку
            isValid: true  // Початковий стан перевірки
        };
    },
    watch: {
        // Стежимо за змінами пропса
        name(newName) {
            this.localName = newName;  // Оновлюємо локальну змінну, коли змінюється пропс
        },
        localName(newLocalName) {
            this.$emit('update:name', newLocalName); // Випромінюємо оновлене значення email
        }
    },
    methods: {
        validateInput() {
            const regex = /^[a-zA-Z0-9_]{3,}$/;  // Перевірка на допустимі символи: літери, цифри та _
            this.isValid = regex.test(this.localName);  // Перевірка локальної змінної
        },
    },
};
</script>
