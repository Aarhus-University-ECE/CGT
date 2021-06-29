


class Player:
    def __init__(self, n_lives) -> None:
        self.lives = n_lives

    def take_damage(self, damage):
        self.lives -= damage
        self.is_alive()

    def is_alive(self):
        if self.lives < 0:
            del self
            return False
        return True

    def __del__(self):
        print("You died")
        

class Game:
    def __init__(self, n_lives) -> None:
        self.player = Player(n_lives)


    def start(self):
        while self.player.is_alive():
            self.process_inputs()
            self.update_game_world()
            self.generate_outputs()

    def process_inputs(self):
        pass

    def update_game_world(self):
        pass

    def generate_outputs(self):
        pass




if __name__ == '__main__':
    n_lives = 3
    game = Game(n_lives)
    