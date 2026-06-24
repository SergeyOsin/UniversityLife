const InputText1 = document.querySelector('#input0');
const OutputText = document.querySelector('#output0');
const button1 = document.querySelector('.create');
const button2 = document.querySelector('.clear');

function SaveTextonUpdate(){
  localStorage.setItem('value',InputText1.value);
}

window.onload=function(){
  if (localStorage.getItem('value'))
    InputText1.value = localStorage.getItem('value');
}

const ClearTextArea = () => {
  if (InputText1.value === "") {
    alert("Текстовое поле уже пустое!");
    return;
  }
  InputText1.value = OutputText.value = "";
  localStorage.setItem('value',InputText1.value);
}

InputText1.addEventListener('input',SaveTextonUpdate);

button2.onclick = ClearTextArea;

button1.addEventListener('click', function () {
  OutputText.value = " ";
  if (InputText1.value === "") {
    alert("Текстовое поле пустое! Введите текст!");
    return;
  }
  let ArrayA = [""];
  let ArrayWords = InputText1.value.match(/[а-яёa-z0-9'-]+/giu);
  for(let i in ArrayWords)
    ArrayWords[i]=ArrayWords[i].toLowerCase();
  let text0 = "Количество слов в тексте: " + ArrayWords.length+'\n';
  ArrayA.push(text0);
  let countRepeatedWords = {};
  ArrayWords.forEach(elem => {
    if (countRepeatedWords[elem])
      countRepeatedWords[elem]++;
    else countRepeatedWords[elem] = 1;
  });
  ArrayA.push("Используемые слова и их количество:\n");
  const sortedWords = Object.entries(countRepeatedWords).sort(
    (a, b) => b[1] - a[1]);
  sortedWords.forEach(([word, count]) => ArrayA.push(word + ': ' + count + '\n'));
  ArrayA.forEach(elem => {
    OutputText.value += elem;
  });
});






