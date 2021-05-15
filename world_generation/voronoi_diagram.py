import random
import numpy as np
import logging
import math
import cv2



class VoronoiDiagram2D:

    def __init__(self, box_size, log_level=logging.ERROR):
        self.box_size = box_size
        self.diagram = np.zeros((box_size,box_size,3), dtype=np.uint8)
        logging.basicConfig(level=log_level, format='%(message)s')

    def _euclid_distance(self, x1, y1, x2, y2):
        distance = math.hypot(x1-x2, y1-y2)
        return distance

    def _manhatten_distance(self, x1, y1, x2, y2):
        distance = abs(x1-x2) + abs(y1-y2)
        return distance

    def create_diagram(self, n_biomes, distance_type="euclid", show_diagram=False):

        self.x, self.y, self.colors = [],[],[]
        while len(self.x) != n_biomes:
            x,y = random.sample(range(0, self.box_size), 1), random.sample(range(0, self.box_size), 1) 
            # Maybe add a line checking that the newly generated random point is not already in the list of generated points.
            color = random.sample(range(0,255),3)
            self.x.append(x[0])
            self.y.append(y[0])
            self.colors.append(color)
            self.diagram[x,y] = color

        # if show_diagram:
        #     cv2.imshow("before", self.diagram)
        #     cv2.waitKey(0)
        #     cv2.destroyAllWindows()

        if distance_type == "manhatten":
            def dist(x1,y1,x2,y2):
                return self._manhatten_distance(x1,y1,x2,y2)
        else:
            def dist(x1,y1,x2,y2):
                return self._euclid_distance(x1,y1,x2,y2)

        def get_indices(inlist,value):
            return {i for i,x in enumerate(inlist) if x == value}

        for x in range(self.box_size):
            for y in range(self.box_size):
                dist_to_biomes = [dist(x,y,x2,y2) for x2,y2 in zip(self.x, self.y)]
                smallest_dist = min(dist_to_biomes)
                closest_biome_idx = dist_to_biomes.index(smallest_dist)
                self.diagram[x,y] = self.colors[closest_biome_idx]


        if show_diagram:
            image = cv2.circle(self.diagram, (self.y[0], self.x[0]), 2, (0,0,0), 2)
            for i in range(1,n_biomes):
                circle_center = (self.y[i], self.x[i])
                image = cv2.circle(image, circle_center, 2, (0,0,0), 2)
            cv2.imshow("Voronoi diagram", image)
            cv2.imwrite("voronoi_diagram.png", image)
            cv2.waitKey(0)
            cv2.destroyAllWindows()    


if __name__=="__main__":
    box_size = 500
    vd = VoronoiDiagram2D(box_size, logging.DEBUG)
    n_biomes = 5 # consisting of e.g.: forests, desert, mountains, grassland
    vd.create_diagram(n_biomes, distance_type="euclid", show_diagram=True)



