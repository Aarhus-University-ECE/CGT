import random
import numpy as np
import logging


class MapGeneration2D:

    def __init__(self, grid_size, log_level=logging.ERROR):
        self.occupancy_grid = np.zeros(shape=(grid_size, grid_size))
        self.grid_size = grid_size
        self.occupied = 1
        logging.basicConfig(level=log_level, format='%(message)s')

    def drunkard_walk(self, cur_pos_x, cur_pos_y, n_nodes):
        logging.debug(f"{cur_pos_x} {cur_pos_y}")
        if self.occupancy_grid[cur_pos_x,cur_pos_y] != self.occupied:
            self.occupancy_grid[cur_pos_x,cur_pos_y] = self.occupied
            n_nodes -= 1
        else:
            n_nodes = n_nodes
        if n_nodes == 0: # base case
            return
        (x,y), direction = self._neighbour_direction(0,4) # we define four directions: N, E, S, W, corresponding to the numbers: 0,1,2,3
        if cur_pos_x+x >= 0 and cur_pos_x+x < self.grid_size and cur_pos_y+y >= 0 and cur_pos_y+y < self.grid_size: # ensure we don't move out of the valid grid space
            cur_pos_x += x
            cur_pos_y += y
        logging.debug(f"{direction}")
        return self.drunkard_walk(cur_pos_x, cur_pos_y, n_nodes)

    def _neighbour_direction(self, lower_range, upper_range):
        """
        maps from a direction number to x,y increment/decrements for a 2d grid
        """
        direction = random.randrange(lower_range,upper_range)
        mapping = {0: 'N', 1: 'E', 2: 'S', 3: 'W', 4: 'NE', 5: 'SE', 6: 'SW', 7: 'NW'}
        mapping_direction = mapping[direction]
        direction_mapping = {'N': (-1,0),
                            'E': (0,1),
                            'S': (1,0),
                            'W': (0,-1),
                            'NE': (-1,1),
                            'SE': (1,1),
                            'SW': (1,-1),
                            'NW': (-1,-1)}
        return direction_mapping[mapping_direction], mapping_direction
            


if __name__=="__main__":
    grid_size = 5
    mg = MapGeneration2D(grid_size, logging.DEBUG)
    start_pos = int(np.floor(grid_size/2))
    mg.drunkard_walk(start_pos,start_pos,20)
    print(mg.occupancy_grid)
