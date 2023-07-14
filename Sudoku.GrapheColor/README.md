## Sudoku solver

### project realised by :
- TRAORE Abdoulaye
- CHACHA Ali Ahmed Bachir
- Soh Denis 
- BENDDIF Basma
- AMRANI Sohaib


Solver choisi : <h3 style="color: cadetblue"> Coloration de graphe <H3>

la coloration de graphe consiste � attribuer une couleur � chacun de ses sommets de mani�re que deux sommets reli�s par une ar�te soient de couleur diff�rente

Ce solver contient diff�rentes classes :

la classe Graphe.cs

```
Cett classe contient

Constructeur Graphe(SudokuGrid grid) : Initialise un nouvel objet Graphe avec une grille Sudoku sp�cifi�e. La grille est utilis�e pour initialiser les sommets du graphe.

M�thode getGrid() : Renvoie l'objet SudokuGrid contenant la grille Sudoku associ�e au graphe.

M�thode displayGrid() : Affiche la grille Sudoku du graphe dans la console. Cette m�thode est utilis�e � des fins de d�bogage pour visualiser l'�tat actuel du graphe.

M�thode algoNaifOptimise() : Applique un algorithme na�f optimis� pour r�soudre le Sudoku repr�sent� par le graphe. L'algorithme essaie de remplir les cases du Sudoku en testant diff�rentes combinaisons de couleurs. La solution trouv�e est ensuite mise � jour dans la grille associ�e au graphe.

M�thode attribuerCouleurGraphe(int nbOk, int nbCouleurs, int couleur) : Une m�thode r�cursive utilis�e par l'algorithme pour attribuer une couleur � chaque case du Sudoku. Elle parcourt les sommets du graphe et teste diff�rentes combinaisons de couleurs pour remplir les cases. La m�thode renvoie true si le Sudoku a pu �tre enti�rement compl�t�, sinon elle renvoie false.

```

La classe Sommet.cs

```
Cette classe contient

Constructeur Sommet(int id, int couleur) : Initialise un nouvel objet Sommet avec un identifiant et une couleur sp�cifi�s. L'identifiant repr�sente l'indice de la case dans la grille Sudoku (de 0 � 80). La couleur repr�sente la couleur affect�e au sommet (ou 0 si aucune couleur n'a �t� affect�e).

M�thode getCouleur() : Renvoie la couleur actuellement affect�e au sommet. Par convention, la valeur 0 indique qu'aucune couleur n'a �t� affect�e.

M�thode setCouleur(int couleur) : Affecte une couleur sp�cifi�e au sommet.

M�thode testCouleur(int couleur) : Teste si l'affectation d'une couleur sp�cifi�e au sommet est en conflit avec les sommets adjacents. La m�thode renvoie true si la couleur n'est pas en conflit avec les adjacents, sinon elle renvoie false.

M�thode determineAdjacents(List<Sommet> sommets) : D�termine les sommets adjacents au sommet courant. Cette m�thode prend en param�tre une liste de sommets et met � jour la liste des adjacents du sommet courant en fonction des positions de ligne, colonne et bloc.
```

Et enfin la classe GrapheSolver.cs

```
Cette classe impl�mente la m�thode Solve de l'interface ISudokuSolver. 
Elle cr�e un objet Graphe � partir de la grille Sudoku fournie, puis effectue la coloration du graphe en utilisant l'algorithme na�f optimis�. 
Enfin, elle affiche la grille compl�t�e et renvoie la grille r�solue. 
Si une exception est lev�e pendant le processus de r�solution, elle est affich�e � l'utilisateur.
```
