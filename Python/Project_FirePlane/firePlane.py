# 1.创建窗口
# 2.加载背景图
# 3.背景图片贴到窗口
# 4.刷新窗口
import pygame
from pygame.locals import *
import time
import random
import sys

# 模拟常量，第一后不再更改
WINDOW_WIDTH = 512
WINDOW_HEIGHT = 768

enemy_list = []
score = 0

class HeroBullet:
    def __init__(self, img_path, x, y, window):
        self.img = pygame.image.load(img_path)
        self.x = x
        self.y = y
        self.window = window

    def display(self):
        """贴图"""
        self.window.blit(self.img, (self.x, self.y))

    def move(self):
        self.y -= 10

    def is_hit_enemy(self, enemy):
        if pygame.Rect.colliderect\
        (
            pygame.Rect(self.x, self.y, 20, 31),
            pygame.Rect(enemy.x, enemy.y, 100, 68)
        ):
            return True
        else:
            return False

class BasePlane:

    def __init__(self, img_path, x, y, window):
        self.img = pygame.image.load(img_path)
        self.x = x
        self.y = y
        self.window = window

    def display(self):
        """贴图"""
        self.window.blit(self.img, (self.x, self.y))


class EnemyPlane(BasePlane):
    """敌人飞机类"""

    def __init__(self, img_path, x, y, window):
        self.img = pygame.image.load(img_path)
        self.x = x
        self.y = y
        self.window = window
        self.is_hited = False

    def move(self):
        self.y += 4
        # 到达窗口下边界，回到顶部
        if self.y >= WINDOW_HEIGHT:
            self.y = 0
            self.x = random.randint(0, 412)

    def moveFast(self):
        self.y += 6
        # 到达窗口下边界，回到顶部
        if self.y >= WINDOW_HEIGHT:
            self.y = 0
            self.x = random.randint(0, 412)

    def moveFastest(self):
        self.y += 8
        # 到达窗口下边界，回到顶部
        if self.y >= WINDOW_HEIGHT:
            self.y = 0
            self.x = random.randint(0, 412)

    def display(self):
        """贴图"""
        if self.is_hited:
            self.y = 0
            self.x = random.randint(0, 412)
            self.is_hited = False
        self.window.blit(self.img, (self.x, self.y))


class HeroPlane(BasePlane):
    def __init__(self, img_path, x, y, window):
        self.img = pygame.image.load(img_path)
        self.x = x
        self.y = y
        self.window = window
        self.bullets = []  # 记录飞机所有子弹

    def diplay_bullets(self):
        deleted_bullets = []
        for bullet in self.bullets:
            # 判断子弹是否超出上边界
            if bullet.y >= -31:
                bullet.display()
                bullet.move()
            else:
                deleted_bullets.append(bullet)
            for enemy in enemy_list:
                if bullet.is_hit_enemy(enemy):
                    global score
                    score += 10
                    enemy.is_hited = True
                    deleted_bullets.append(bullet)
                    break
        for out_window_bullet in deleted_bullets:
            self.bullets.remove(out_window_bullet)

    def move_left(self):
        if self.x >= 5:
            self.x += -6

    def move_right(self):
        if self.x <= 407:
            self.x += 6

    def move_up(self):
        if self.y >= 280:
            self.y += -6

    def move_down(self):
        if self.y <= 700:
            self.y += 6

    def fire(self):
        """发射子弹"""
        bullet = HeroBullet("res/fire_plane/assisent1_3.png", self.x + 17, self.y - 34, self.window)
        bullet.display()
        self.bullets.append(bullet)
        print(len(self.bullets))
        """子弹音效"""
        fire_sound = pygame.mixer.Sound('res/fire_plane/music/fire_heroPlae.wav')
        fire_sound.set_volume(0.1)
        fire_sound.play(0, 0)
        # self.eee('res/fire_plane/music/fire_heroPlae.wav') 测试阶段

    # def eee(path):  测试阶段
    #     fire_sound = pygame.mixer.Sound(path)
    #     fire_sound.set_volume(0.1)
    #     fire_sound.play()

def main():
    # 1.初始化pygame库，让计算机硬件准备，声音，文字
    pygame.init()
    pygame.mixer.init()  # 背景音乐控制模块
    pygame.mixer.music.load('res/fire_plane/music/background_music.mp3')
    pygame.mixer.music.set_volume(0.15)  # 背景音乐音量控制
    pygame.mixer.music.play(-1, 10)  # 背景音乐播放控制  （-1为循环播放，10为从第10秒开始）
    # 2.创建窗口
    window = pygame.display.set_mode((WINDOW_WIDTH, WINDOW_HEIGHT))
    # 3.加载图片文件，返回图片对象
    bg_img = pygame.image.load("res/fire_plane/bg1.jpg")
    # 创建对象
    hero_plane = HeroPlane("res/fire_plane/gra_planeImg0.png", 240, 500, window)
    enemy_plane = EnemyPlane("res/fire_plane/enemy2.png", 20, 0, window)
    enemy_plane2 = EnemyPlane("res/fire_plane/enemy_other_3.png", 80, 0, window)
    enemy_plane3 = EnemyPlane("res/fire_plane/enemy_other_2.png", 140, 0, window)
    enemy_plane4 = EnemyPlane("res/fire_plane/enemy13_0.png", 200, 0, window)
    enemy_list.append(enemy_plane)
    enemy_list.append(enemy_plane2)
    enemy_list.append(enemy_plane3)
    enemy_list.append(enemy_plane4)
    score_font = pygame.font.Font("res/fire_plane/UniTortred.TTF", 40)
    while True:
        # 4.贴图（制定坐标，将图片绘制到窗口)
        window.blit(bg_img, (0, 0))
        hero_plane.display()
        hero_plane.diplay_bullets()
        for enemyPlane in enemy_list:
            enemyPlane.display()
            if score < 300:
                enemyPlane.move()
            elif score < 500:
                enemyPlane.moveFast()
            else:
                enemyPlane.moveFastest()
        score_text = score_font.render("Score:%d" % score, 1, (255, 255, 255))
        window.blit(score_text, (10, 10))

        # 5.刷新界面
        pygame.display.update()
        for event in pygame.event.get():
            # 判断是否点击了退出按钮
            if event.type == QUIT:
                # 让程序终止
                sys.exit()
                pygame.quit()
            elif event.type == KEYDOWN:#有按键输入的话
                if event.key == K_SPACE:
                    hero_plane.fire()
        pressed_key = pygame.key.get_pressed()
        if pressed_key[pygame.K_LEFT]:
            hero_plane.move_left()
        if pressed_key[pygame.K_RIGHT]:
            hero_plane.move_right()
        if pressed_key[pygame.K_UP]:
            hero_plane.move_up()
        if pressed_key[pygame.K_DOWN]:
            hero_plane.move_down()
        time.sleep(0.01)

main()