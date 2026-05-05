const canvas = document.getElementById("game")
const ctx = canvas.getContext("2d")

const overlay = document.getElementById("overlay")
const startButton = document.getElementById("startButton")
const restartButton = document.getElementById("restartButton")
const coinsLabel = document.getElementById("coins")
const scoreLabel = document.getElementById("score")
const livesLabel = document.getElementById("lives")

const WIDTH = canvas.width
const HEIGHT = canvas.height
const GROUND_Y = 470
const WORLD_WIDTH = 4200

const keys = {
  left: false,
  right: false,
  jump: false,
}

const state = {
  mode: "menu",
  messageTitle: "Супер Марио",
  messageText: "Събери монетите и стигни до флага.",
}

let cameraX = 0
let level
let player

function createLevel() {
  const platforms = [
    { x: 0, y: GROUND_Y, w: 720, h: 70, type: "ground" },
    { x: 880, y: GROUND_Y, w: 700, h: 70, type: "ground" },
    { x: 1720, y: GROUND_Y, w: 820, h: 70, type: "ground" },
    { x: 2720, y: GROUND_Y, w: 760, h: 70, type: "ground" },
    { x: 3620, y: GROUND_Y, w: 580, h: 70, type: "ground" },

    { x: 320, y: 380, w: 120, h: 24, type: "brick" },
    { x: 460, y: 325, w: 120, h: 24, type: "brick" },
    { x: 1050, y: 360, w: 120, h: 24, type: "brick" },
    { x: 1240, y: 300, w: 120, h: 24, type: "brick" },
    { x: 1450, y: 250, w: 120, h: 24, type: "brick" },
    { x: 1880, y: 390, w: 180, h: 24, type: "brick" },
    { x: 2130, y: 325, w: 120, h: 24, type: "question" },
    { x: 2360, y: 270, w: 160, h: 24, type: "brick" },
    { x: 2900, y: 360, w: 120, h: 24, type: "brick" },
    { x: 3090, y: 305, w: 120, h: 24, type: "question" },
    { x: 3290, y: 250, w: 120, h: 24, type: "brick" },
  ]

  const coins = [
    { x: 380, y: 335, r: 12, collected: false },
    { x: 520, y: 280, r: 12, collected: false },
    { x: 1090, y: 315, r: 12, collected: false },
    { x: 1280, y: 255, r: 12, collected: false },
    { x: 1490, y: 205, r: 12, collected: false },
    { x: 1940, y: 345, r: 12, collected: false },
    { x: 2010, y: 345, r: 12, collected: false },
    { x: 2190, y: 280, r: 12, collected: false },
    { x: 2400, y: 225, r: 12, collected: false },
    { x: 2480, y: 225, r: 12, collected: false },
    { x: 2940, y: 315, r: 12, collected: false },
    { x: 3130, y: 260, r: 12, collected: false },
    { x: 3330, y: 205, r: 12, collected: false },
    { x: 3750, y: 390, r: 12, collected: false },
    { x: 3850, y: 390, r: 12, collected: false },
  ]

  const enemies = [
    { x: 560, y: 430, w: 38, h: 38, minX: 500, maxX: 650, speed: 1.2, dir: 1, alive: true },
    { x: 1160, y: 430, w: 38, h: 38, minX: 960, maxX: 1420, speed: 1.4, dir: -1, alive: true },
    { x: 2000, y: 430, w: 38, h: 38, minX: 1800, maxX: 2450, speed: 1.3, dir: 1, alive: true },
    { x: 2990, y: 320, w: 38, h: 38, minX: 2900, maxX: 3080, speed: 1.1, dir: -1, alive: true },
    { x: 3750, y: 430, w: 38, h: 38, minX: 3680, maxX: 4050, speed: 1.5, dir: 1, alive: true },
  ]

  return {
    platforms,
    coins,
    enemies,
    goal: { x: 3980, y: 260, w: 18, h: 210 },
    castle: { x: 4050, y: 270, w: 100, h: 200 },
    clouds: [
      { x: 150, y: 90, size: 0.9 },
      { x: 700, y: 120, size: 1.2 },
      { x: 1450, y: 100, size: 0.95 },
      { x: 2250, y: 85, size: 1.1 },
      { x: 3050, y: 120, size: 0.85 },
      { x: 3800, y: 95, size: 1.05 },
    ],
    hills: [
      { x: 70, w: 260, h: 130, color: "#74c365" },
      { x: 840, w: 280, h: 120, color: "#78c96d" },
      { x: 1680, w: 220, h: 110, color: "#67ba58" },
      { x: 2600, w: 260, h: 145, color: "#74c365" },
      { x: 3500, w: 240, h: 120, color: "#68ba5a" },
    ],
  }
}

function createPlayer() {
  return {
    x: 80,
    y: 380,
    w: 38,
    h: 54,
    vx: 0,
    vy: 0,
    speed: 4.2,
    jumpForce: 12.5,
    onGround: false,
    facing: 1,
    coins: 0,
    score: 0,
    lives: 3,
    invulnerableUntil: 0,
  }
}

function startGame() {
  level = createLevel()
  player = createPlayer()
  cameraX = 0
  state.mode = "playing"
  hideOverlay()
  updateHud()
}

function showOverlay(title, text) {
  state.messageTitle = title
  state.messageText = text
  overlay.innerHTML = `
    <div class="overlay-card">
      <h1>${title}</h1>
      <p>${text}</p>
      <p>Движение: <strong>A/D</strong> или <strong>стрелки</strong>, скок: <strong>W</strong>, <strong>нагоре</strong> или <strong>Space</strong>.</p>
      <div class="actions">
        <button id="startButton">Старт</button>
        <button id="restartButton" class="secondary">Нова игра</button>
      </div>
    </div>
  `
  overlay.classList.remove("hidden")
  overlay.querySelector("#startButton").addEventListener("click", startGame)
  overlay.querySelector("#restartButton").addEventListener("click", startGame)
}

function hideOverlay() {
  overlay.classList.add("hidden")
}

function updateHud() {
  coinsLabel.textContent = player.coins
  scoreLabel.textContent = player.score
  livesLabel.textContent = player.lives
}

function clamp(value, min, max) {
  return Math.max(min, Math.min(max, value))
}

function intersects(a, b) {
  return (
    a.x < b.x + b.w &&
    a.x + a.w > b.x &&
    a.y < b.y + b.h &&
    a.y + a.h > b.y
  )
}

function resetPlayerPosition() {
  player.x = 80
  player.y = 380
  player.vx = 0
  player.vy = 0
  cameraX = 0
}

function loseLife() {
  const now = performance.now()
  if (now < player.invulnerableUntil) {
    return
  }

  player.lives -= 1
  player.invulnerableUntil = now + 1600
  updateHud()

  if (player.lives <= 0) {
    state.mode = "gameover"
    showOverlay("Край на играта", "Опитай пак и стигни до флага.")
    return
  }

  resetPlayerPosition()
}

function handleInput() {
  if (keys.left && !keys.right) {
    player.vx = -player.speed
    player.facing = -1
  } else if (keys.right && !keys.left) {
    player.vx = player.speed
    player.facing = 1
  } else {
    player.vx *= 0.7
    if (Math.abs(player.vx) < 0.2) {
      player.vx = 0
    }
  }

  if (keys.jump && player.onGround) {
    player.vy = -player.jumpForce
    player.onGround = false
  }
}

function solveHorizontalCollisions() {
  for (const platform of level.platforms) {
    if (!intersects(player, platform)) {
      continue
    }

    if (player.vx > 0) {
      player.x = platform.x - player.w
    } else if (player.vx < 0) {
      player.x = platform.x + platform.w
    }
    player.vx = 0
  }
}

function solveVerticalCollisions(previousY) {
  player.onGround = false

  for (const platform of level.platforms) {
    if (!intersects(player, platform)) {
      continue
    }

    const wasAbove = previousY + player.h <= platform.y
    const wasBelow = previousY >= platform.y + platform.h

    if (player.vy >= 0 && wasAbove) {
      player.y = platform.y - player.h
      player.vy = 0
      player.onGround = true
    } else if (player.vy < 0 && wasBelow) {
      player.y = platform.y + platform.h
      player.vy = 0
    }
  }
}

function updateCoins() {
  for (const coin of level.coins) {
    if (coin.collected) {
      continue
    }

    const coinBox = {
      x: coin.x - coin.r,
      y: coin.y - coin.r,
      w: coin.r * 2,
      h: coin.r * 2,
    }

    if (intersects(player, coinBox)) {
      coin.collected = true
      player.coins += 1
      player.score += 100
      updateHud()
    }
  }
}

function updateEnemies() {
  const previousBottom = player.y + player.h - player.vy

  for (const enemy of level.enemies) {
    if (!enemy.alive) {
      continue
    }

    enemy.x += enemy.speed * enemy.dir
    if (enemy.x <= enemy.minX || enemy.x + enemy.w >= enemy.maxX) {
      enemy.dir *= -1
    }

    if (!intersects(player, enemy)) {
      continue
    }

    const stomped = player.vy > 0 && previousBottom <= enemy.y + 10
    if (stomped) {
      enemy.alive = false
      player.vy = -8.5
      player.score += 200
      updateHud()
    } else {
      loseLife()
    }
  }
}

function updateGoal() {
  const flagBox = {
    x: level.goal.x - 16,
    y: level.goal.y,
    w: 32,
    h: level.goal.h,
  }

  if (intersects(player, flagBox)) {
    state.mode = "won"
    player.score += 500
    updateHud()
    showOverlay("Победа!", "Ти стигна до флага и завърши нивото.")
  }
}

function update() {
  if (state.mode !== "playing") {
    return
  }

  handleInput()

  player.vy += 0.6
  player.vy = Math.min(player.vy, 14)

  player.x += player.vx
  player.x = clamp(player.x, 0, WORLD_WIDTH - player.w)
  solveHorizontalCollisions()

  const previousY = player.y
  player.y += player.vy
  solveVerticalCollisions(previousY)

  updateCoins()
  updateEnemies()
  updateGoal()

  if (player.y > HEIGHT + 180) {
    loseLife()
  }

  cameraX = clamp(player.x - WIDTH * 0.35, 0, WORLD_WIDTH - WIDTH)
}

function drawCloud(x, y, scale) {
  ctx.save()
  ctx.translate(x, y)
  ctx.scale(scale, scale)
  ctx.fillStyle = "rgba(255, 255, 255, 0.95)"
  ctx.beginPath()
  ctx.arc(0, 0, 24, 0, Math.PI * 2)
  ctx.arc(26, -8, 22, 0, Math.PI * 2)
  ctx.arc(54, 0, 24, 0, Math.PI * 2)
  ctx.arc(30, 12, 26, 0, Math.PI * 2)
  ctx.fill()
  ctx.restore()
}

function drawBackground() {
  const sky = ctx.createLinearGradient(0, 0, 0, HEIGHT)
  sky.addColorStop(0, "#86dbff")
  sky.addColorStop(1, "#bfeeff")
  ctx.fillStyle = sky
  ctx.fillRect(0, 0, WIDTH, HEIGHT)

  ctx.fillStyle = "#fff2a8"
  ctx.beginPath()
  ctx.arc(820, 88, 42, 0, Math.PI * 2)
  ctx.fill()

  for (const cloud of level.clouds) {
    drawCloud(cloud.x - cameraX * 0.35, cloud.y, cloud.size)
  }

  for (const hill of level.hills) {
    const hillX = hill.x - cameraX * 0.6
    ctx.fillStyle = hill.color
    ctx.beginPath()
    ctx.moveTo(hillX, HEIGHT)
    ctx.quadraticCurveTo(hillX + hill.w * 0.5, HEIGHT - hill.h, hillX + hill.w, HEIGHT)
    ctx.closePath()
    ctx.fill()
  }
}

function drawPlatform(platform) {
  const x = platform.x - cameraX
  const y = platform.y

  if (platform.type === "ground") {
    ctx.fillStyle = "#8f5a31"
    ctx.fillRect(x, y, platform.w, platform.h)
    ctx.fillStyle = "#62bf4d"
    ctx.fillRect(x, y, platform.w, 16)
    ctx.fillStyle = "#754421"
    for (let i = 0; i < platform.w; i += 32) {
      ctx.fillRect(x + i, y + 24, 20, 6)
      ctx.fillRect(x + i + 8, y + 44, 18, 6)
    }
    return
  }

  ctx.fillStyle = platform.type === "question" ? "#f3c13a" : "#b66731"
  ctx.fillRect(x, y, platform.w, platform.h)
  ctx.strokeStyle = "#6a3415"
  ctx.lineWidth = 3
  ctx.strokeRect(x, y, platform.w, platform.h)

  for (let i = 0; i < platform.w; i += 40) {
    ctx.beginPath()
    ctx.moveTo(x + i + 8, y + 6)
    ctx.lineTo(x + i + 24, y + 18)
    ctx.stroke()
  }

  if (platform.type === "question") {
    ctx.fillStyle = "#6a3415"
    ctx.font = "bold 20px Arial"
    ctx.fillText("?", x + platform.w / 2 - 6, y + 18)
  }
}

function drawCoin(coin) {
  if (coin.collected) {
    return
  }
  const pulse = 1 + Math.sin(performance.now() / 120) * 0.08
  ctx.save()
  ctx.translate(coin.x - cameraX, coin.y)
  ctx.scale(pulse, pulse)
  ctx.fillStyle = "#ffd84d"
  ctx.beginPath()
  ctx.ellipse(0, 0, coin.r, coin.r + 3, 0, 0, Math.PI * 2)
  ctx.fill()
  ctx.strokeStyle = "#c98b14"
  ctx.lineWidth = 3
  ctx.stroke()
  ctx.restore()
}

function drawEnemy(enemy) {
  if (!enemy.alive) {
    return
  }

  const x = enemy.x - cameraX
  const y = enemy.y
  ctx.fillStyle = "#8b4f28"
  ctx.beginPath()
  ctx.roundRect(x, y + 8, enemy.w, enemy.h - 8, 10)
  ctx.fill()

  ctx.fillStyle = "#f1d6ad"
  ctx.fillRect(x + 6, y + 18, enemy.w - 12, 12)

  ctx.fillStyle = "#1e1e1e"
  ctx.fillRect(x + 8, y + 16, 5, 5)
  ctx.fillRect(x + enemy.w - 13, y + 16, 5, 5)
  ctx.fillRect(x + 7, y + enemy.h - 4, 10, 4)
  ctx.fillRect(x + enemy.w - 17, y + enemy.h - 4, 10, 4)
}

function drawPlayer() {
  const flashing = performance.now() < player.invulnerableUntil && Math.floor(performance.now() / 100) % 2 === 0
  if (flashing) {
    return
  }

  const x = player.x - cameraX
  const y = player.y

  ctx.fillStyle = "#db2d2d"
  ctx.fillRect(x + 7, y, 24, 14)
  ctx.fillRect(x + 3, y + 10, 30, 8)

  ctx.fillStyle = "#f4d2af"
  ctx.fillRect(x + 8, y + 14, 22, 18)

  ctx.fillStyle = "#2449b9"
  ctx.fillRect(x + 8, y + 32, 22, 16)

  ctx.fillStyle = "#5e3118"
  ctx.fillRect(x + 2, y + 44, 12, 10)
  ctx.fillRect(x + 24, y + 44, 12, 10)

  ctx.fillStyle = "#603316"
  const moustacheX = player.facing === 1 ? x + 18 : x + 10
  ctx.fillRect(moustacheX, y + 24, 10, 4)

  ctx.fillStyle = "#ffffff"
  ctx.fillRect(x + (player.facing === 1 ? 21 : 13), y + 18, 5, 5)
}

function drawFlagAndCastle() {
  const poleX = level.goal.x - cameraX
  ctx.fillStyle = "#f4f4f4"
  ctx.fillRect(poleX, level.goal.y, level.goal.w, level.goal.h)

  ctx.fillStyle = "#43c95b"
  ctx.beginPath()
  ctx.moveTo(poleX + level.goal.w, level.goal.y + 30)
  ctx.lineTo(poleX + level.goal.w + 52, level.goal.y + 52)
  ctx.lineTo(poleX + level.goal.w, level.goal.y + 74)
  ctx.closePath()
  ctx.fill()

  const castleX = level.castle.x - cameraX
  ctx.fillStyle = "#a88372"
  ctx.fillRect(castleX, level.castle.y, level.castle.w, level.castle.h)
  ctx.fillStyle = "#866457"
  ctx.fillRect(castleX + 20, level.castle.y - 30, 18, 30)
  ctx.fillRect(castleX + 62, level.castle.y - 30, 18, 30)
  ctx.fillStyle = "#5c3a30"
  ctx.fillRect(castleX + 36, level.castle.y + 120, 28, 80)
}

function drawHudHint() {
  if (state.mode !== "playing") {
    return
  }
  ctx.fillStyle = "rgba(16, 35, 63, 0.65)"
  ctx.font = "bold 18px Arial"
  ctx.fillText("Стигни до флага ->", WIDTH - 190, 40)
}

function draw() {
  if (!level || !player) {
    return
  }

  drawBackground()

  for (const platform of level.platforms) {
    drawPlatform(platform)
  }

  for (const coin of level.coins) {
    drawCoin(coin)
  }

  for (const enemy of level.enemies) {
    drawEnemy(enemy)
  }

  drawFlagAndCastle()
  drawPlayer()
  drawHudHint()
}

function loop() {
  update()
  draw()
  requestAnimationFrame(loop)
}

function setKeyState(code, isPressed) {
  if (code === "ArrowLeft" || code === "KeyA") {
    keys.left = isPressed
  }
  if (code === "ArrowRight" || code === "KeyD") {
    keys.right = isPressed
  }
  if (code === "ArrowUp" || code === "KeyW" || code === "Space") {
    keys.jump = isPressed
  }

  if ((state.mode === "menu" || state.mode === "gameover" || state.mode === "won") && isPressed && code === "Enter") {
    startGame()
  }
}

window.addEventListener("keydown", (event) => {
  if (["ArrowLeft", "ArrowRight", "ArrowUp", "Space"].includes(event.code)) {
    event.preventDefault()
  }
  setKeyState(event.code, true)
})

window.addEventListener("keyup", (event) => {
  setKeyState(event.code, false)
})

startButton.addEventListener("click", startGame)
restartButton.addEventListener("click", startGame)

level = createLevel()
player = createPlayer()
updateHud()
showOverlay(state.messageTitle, state.messageText)
loop()
