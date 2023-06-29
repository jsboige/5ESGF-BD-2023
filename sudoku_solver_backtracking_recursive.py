def read_sudoku_from_file(file_path):
    with open(file_path, 'r') as file:
        lines = file.readlines()
    
    sudoku = [[int(digit) for digit in line.strip()] for line in lines]
    return sudoku


def is_valid(sudoku, row, col, digit):
    
    for i in range(9):
        if sudoku[row][i] == digit:
            return False
    
    
    for i in range(9):
        if sudoku[i][col] == digit:
            return False
    
    
    start_row = (row // 3) * 3
    start_col = (col // 3) * 3
    for i in range(3):
        for j in range(3):
            if sudoku[start_row + i][start_col + j] == digit:
                return False
    
    return True


def find_empty_cell(sudoku):
    for row in range(9):
        for col in range(9):
            if sudoku[row][col] == 0:
                return row, col
    return None, None


def solve_sudoku(sudoku):
    row, col = find_empty_cell(sudoku)
    
    
    if row is None:
        return True
    
    
    for digit in range(1, 10):
        if is_valid(sudoku, row, col, digit):
            sudoku[row][col] = digit
            
            if solve_sudoku(sudoku):
                return True
            
            sudoku[row][col] = 0
    
    return False


def print_sudoku(sudoku):
    for row in sudoku:
        print(' '.join(map(str, row)))


# Main 
file_path = './Puzzles/Sudoku_Easy51.txt'   

sudoku = read_sudoku_from_file(file_path)

print("Sudoku:")
print_sudoku(sudoku)

print("\nRésolution Sudoku...")
if solve_sudoku(sudoku):
    print("\nSudoku résolu:")
    print_sudoku(sudoku)
else:
    print("\nErreur.")


