C = [4; 6];
A = [1,1; 0.1,0.2; 0.05,0.02];
B = [3000;400;100];
lower_x = [];
upper_x = [];
ctype = "UUU";
vartype = "CC";
sense = -1;
[x_opt, f_max, error_number, extra_info] = glpk (C, A, B, lower_x, upper_x, ctype,
vartype, sense)
