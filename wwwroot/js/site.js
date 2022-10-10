const uri = 'api/universidade'
let cursos = []
let alunos = []

function getAll () {
  getCursos()
  getAlunos()
}

function getAlunos () {
  fetch(uri + '/aluno')
    .then(response => response.json())
    .then(data => _displayAlunos(data))
    .catch(error => console.error('Unable to get alunos.', error))
}

function getCursos () {
  fetch(uri + '/cursos')
    .then(response => response.json())
    .then(data => _displayCursos(data))
    .catch(error => console.error('Unable to get cursos.', error))
}

function addCurso () {
  const addNameTextbox = document.getElementById('add-curso_nome')
  const addSiglaTextbox = document.getElementById('add-curso_sigla')

  const curso = {
    nome: addNameTextbox.value.trim(),
    sigla: addSiglaTextbox.value.trim()
  }

  fetch(uri + '/cursos', {
    method: 'POST',
    headers: {
      Accept: 'application/json',
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(curso)
  })
    .then(response => response.json())
    .then(() => {
      getCursos()
      addNameTextbox.value = ''
      addSiglaTextbox.value = ''
    })
    .catch(error => console.error('Unable to add curso.', error))
}

function addAluno () {
  const addNameTextbox = document.getElementById('add-aluno_nome')
  const addSiglaTextbox = document.getElementById('add-aluno_curso')

  const aluno = {
    nome: addNameTextbox.value.trim(),
    siglaCurso: addSiglaTextbox.value.trim()
  }

  fetch(uri + '/aluno', {
    method: 'POST',
    headers: {
      Accept: 'application/json',
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(aluno)
  })
    .then(response => response.json())
    .then(() => {
      getAlunos()
      addNameTextbox.value = ''
      addSiglaTextbox.value = ''
    })
    .catch(error => console.error('Unable to add aluno.', error))
}

function deleteAluno (id) {
  fetch(`${uri}` + '/aluno/' + `${id}`, {
    method: 'DELETE'
  })
    .then(() => getAlunos())
    .catch(error => console.error('Unable to delete aluno.', error))
}

function deleteCurso (id) {
  fetch(`${uri}` + '/cursos/' + `${id}`, {
    method: 'DELETE'
  })
    .then(() => getCursos())
    .catch(error => console.error('Unable to delete cursos.', error))
}

function displayEditFormCursos (id) {
  const curso = cursos.find(curso => curso.id === id)

  document.getElementById('edit-curso_id').value = curso.id
  document.getElementById('edit-curso_nome').value = curso.nome
  document.getElementById('edit-curso_sigla').value = curso.sigla
  document.getElementById('editForm-curso').style.display = 'block'
}

function displayEditFormAlunos (id) {
  const aluno = alunos.find(aluno => aluno.id === id)

  document.getElementById('edit-aluno_id').value = aluno.id
  document.getElementById('edit-aluno_nome').value = aluno.nome
  document.getElementById('edit-aluno_curso').value = aluno.siglaCurso
  document.getElementById('editForm-aluno').style.display = 'block'
}

function updateAluno () {
  const alunoId = document.getElementById('edit-aluno_id').value
  const aluno = {
    id: parseInt(alunoId),
    nome: document.getElementById('edit-aluno_nome').value.trim(),
    siglaCurso: document.getElementById('edit-aluno_curso').value.trim()
  }

  fetch(`${uri}` + '/aluno/' + `${aluno.id}`, {
    method: 'PUT',
    headers: {
      Accept: 'application/json',
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(aluno)
  })
    .then(() => getAlunos())
    .catch(error => console.error('Unable to update aluno.', error))

  closeInput()

  return false
}

function updateCurso () {
  const cursoId = document.getElementById('edit-curso_id').value
  const curso = {
    id: parseInt(cursoId),
    nome: document.getElementById('edit-curso_nome').value.trim(),
    sigla: document.getElementById('edit-curso_sigla').value.trim()
  }

  fetch(`${uri}` + '/cursos/' + `${curso.id}`, {
    method: 'PUT',
    headers: {
      Accept: 'application/json',
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(curso)
  })
    .then(() => getCursos())
    .catch(error => console.error('Unable to update cursos.', error))

  closeInput()

  return false
}

function closeInput () {
  document.getElementById('editForm-curso').style.display = 'none'
  document.getElementById('editForm-aluno').style.display = 'none'
}

function _displayCountCursos (itemCount) {
  const name = itemCount === 1 ? 'curso' : 'cursos'

  document.getElementById('counter-cursos').innerText = `${itemCount} ${name}`
}

function _displayCountAlunos (itemCount) {
  const name = itemCount === 1 ? 'aluno' : 'alunos'

  document.getElementById('counter-alunos').innerText = `${itemCount} ${name}`
}

function _displayCursos (data) {
  const tBody = document.getElementById('cursos')
  tBody.innerHTML = ''

  _displayCountCursos(data.length)

  const button = document.createElement('button')

  data.forEach(curso => {
    let editButton = button.cloneNode(false)
    editButton.innerText = 'Edit'
    editButton.classList.add('btn');
    editButton.setAttribute('onclick', `displayEditFormCursos(${curso.id})`)

    let deleteButton = button.cloneNode(false)
    deleteButton.innerText = 'Delete'
    deleteButton.classList.add('btn');
    deleteButton.setAttribute('onclick', `deleteCurso(${curso.id})`)

    let tr = tBody.insertRow()

    let td1 = tr.insertCell(0)
    let textNode1 = document.createTextNode(curso.id)
    td1.appendChild(textNode1)

    let td2 = tr.insertCell(1)
    let textNode2 = document.createTextNode(curso.nome)
    td2.appendChild(textNode2)

    let td3 = tr.insertCell(2)
    let textNode3 = document.createTextNode(curso.sigla)
    td3.appendChild(textNode3)

    let td4 = tr.insertCell(3)
    td4.appendChild(editButton)

    let td5 = tr.insertCell(4)
    td5.appendChild(deleteButton)
  })

  cursos = data
}

function _displayAlunos (data) {
  const tBody = document.getElementById('alunos')
  tBody.innerHTML = ''

  _displayCountAlunos(data.length)

  const button = document.createElement('button')

  data.forEach(aluno => {
    let editButton = button.cloneNode(false)
    editButton.innerText = 'Edit'
    editButton.classList.add('btn');
    editButton.setAttribute('onclick', `displayEditFormAlunos(${aluno.id})`)

    let deleteButton = button.cloneNode(false)
    deleteButton.innerText = 'Delete'
    deleteButton.classList.add('btn');
    deleteButton.setAttribute('onclick', `deleteAluno(${aluno.id})`)

    let tr = tBody.insertRow()

    let td1 = tr.insertCell(0)
    let textNode1 = document.createTextNode(aluno.id)
    td1.appendChild(textNode1)

    let td2 = tr.insertCell(1)
    let textNode2 = document.createTextNode(aluno.nome)
    td2.appendChild(textNode2)

    let td3 = tr.insertCell(2)
    let textNode3 = document.createTextNode(aluno.siglaCurso)
    td3.appendChild(textNode3)

    let td4 = tr.insertCell(3)
    td4.appendChild(editButton)

    let td5 = tr.insertCell(4)
    td5.appendChild(deleteButton)
  })

  alunos = data
}
