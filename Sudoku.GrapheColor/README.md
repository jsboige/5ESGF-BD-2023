## Sudoku solver

### project realised by :
- TRAORE Abdoulaye
- CHACHA Ali Ahmed Bachir
- Soh Denis 
- BENDDIF Basma
- AMRANI Sohaib


Solver choisi : <h3 style="color: cadetblue"> Coloration de graphe <H3>

la coloration de graphe consiste à attribuer une couleur à chacun de ses sommets de manière que deux sommets reliés par une arête soient de couleur différente

Ce solver contient différentes classes :

la classe Graphe.cs

```
Cett classe contient

Constructeur Graphe(SudokuGrid grid) : Initialise un nouvel objet Graphe avec une grille Sudoku spécifiée. La grille est utilisée pour initialiser les sommets du graphe.

Méthode getGrid() : Renvoie l'objet SudokuGrid contenant la grille Sudoku associée au graphe.

Méthode displayGrid() : Affiche la grille Sudoku du graphe dans la console. Cette méthode est utilisée à des fins de débogage pour visualiser l'état actuel du graphe.

Méthode algoNaifOptimise() : Applique un algorithme naïf optimisé pour résoudre le Sudoku représenté par le graphe. L'algorithme essaie de remplir les cases du Sudoku en testant différentes combinaisons de couleurs. La solution trouvée est ensuite mise à jour dans la grille associée au graphe.

Méthode attribuerCouleurGraphe(int nbOk, int nbCouleurs, int couleur) : Une méthode récursive utilisée par l'algorithme pour attribuer une couleur à chaque case du Sudoku. Elle parcourt les sommets du graphe et teste différentes combinaisons de couleurs pour remplir les cases. La méthode renvoie true si le Sudoku a pu être entièrement complété, sinon elle renvoie false.

```

La classe Sommet.cs

```
Cette classe contient

Constructeur Sommet(int id, int couleur) : Initialise un nouvel objet Sommet avec un identifiant et une couleur spécifiés. L'identifiant représente l'indice de la case dans la grille Sudoku (de 0 à 80). La couleur représente la couleur affectée au sommet (ou 0 si aucune couleur n'a été affectée).

Méthode getCouleur() : Renvoie la couleur actuellement affectée au sommet. Par convention, la valeur 0 indique qu'aucune couleur n'a été affectée.

Méthode setCouleur(int couleur) : Affecte une couleur spécifiée au sommet.

Méthode testCouleur(int couleur) : Teste si l'affectation d'une couleur spécifiée au sommet est en conflit avec les sommets adjacents. La méthode renvoie true si la couleur n'est pas en conflit avec les adjacents, sinon elle renvoie false.

Méthode determineAdjacents(List<Sommet> sommets) : Détermine les sommets adjacents au sommet courant. Cette méthode prend en paramètre une liste de sommets et met à jour la liste des adjacents du sommet courant en fonction des positions de ligne, colonne et bloc.
```

Et enfin la classe GrapheSolver.cs

```
Cette classe implémente la méthode Solve de l'interface ISudokuSolver. 
Elle crée un objet Graphe à partir de la grille Sudoku fournie, puis effectue la coloration du graphe en utilisant l'algorithme naïf optimisé. 
Enfin, elle affiche la grille complétée et renvoie la grille résolue. 
Si une exception est levée pendant le processus de résolution, elle est affichée à l'utilisateur.
```
