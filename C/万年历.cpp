#include<stdio.h>
#include<stdlib.h>
int readYear();//读取用户输入的年份
int readMonth();//读取用户输入的月份
int readSelect();//读取用户的选择 
int readChoose();//读取用户是否继续进行查询 
void printMonth(int year, int month) ;//打印日历
void printMonthTitle(int year, int month) ;//打印日历标题
void printMonthBody(int year, int month) ;//打印日历主体 
void printYear(int year, int month1);//打印年历
void printYearTitle(int year, int month1);//打印年历标题
void printYearBody(int year, int month1);//打印年历主体 
int getStartDay(int year, int month) ;//获取year年month月的第一天的星期数
int getTotalNumberOfDays(int year, int month) ;//获取从1800年1月1日(星期三)到year年month月1日的相隔总天数
int getNumberOfDaysInMonth(int year, int month) ;//获取year年month月的总天数
int isLeapYear(int year) ;//判断year年是否为闰年（如果为闰年，则返回1，否则返回0）

int main()
{
	int year, month, select, i, choose, month1;
	
	printf("欢迎使用振豪万年历 ^_^\n"); 
	do
	{
		do
		{	
			printf("请选择你想要查询的模式：\n  1、年历    2、日历\n");
			select = readSelect();
			while (select == 3)
			{
				printf("你所输入的模式指令有误\n");
				printf("请重新输入你要查询的模式：\n  1、年历    2、日历\n");
				select = readSelect();
			}
			if (select != 3)
			{
				printf("输入你要查询的年份，(例：2016  范围：1800-？？)\n");
				year = readYear();
				while (year == 3)//将数值3定为错误代码 
				{
					printf("你所输入的年份超出限制范围\n");
					printf("请重新输入你要查询的年份  (例：2016  范围：1800-？？)\n"); 
					year = readYear();
				}
				while (year != 3)//将数值3定为错误代码 
				{
					if ( select == 2)
					{
						printf("输入你要查询的月份，例：12  范围：1-12\n");
						month = readMonth();
						while (month == -1)//将数值-1定为错误代码 
						{
							printf("你所输入的月份超出限制范围\n");
							printf("请重新输入你要查询的月份  (例：12  范围：1-12)\n"); 
							month = readMonth();
						}
						while (month != -1)
						{
							printMonth(year, month);
							printf("\n\n");//使界面美观 
							break;
						}		
					}
					else 
					{	
						month1 = 1;	
						for (i = 1; i < 13; i++)
						{
							printYear(year, month1);
							month1 += 1;
						}
					}
					break;	
				}
			}
			printf("请问还要继续查询吗？\n  1、继续  2、退出\n");
			choose = readChoose();
		}
		while (choose == 1);
		if (choose == 3) //将数值3定为错误代码
		{
			printf("你所输入的指令有误\n你还剩下1次修改机会\n");
			printf("请重新输入指令\n   1、继续  2、退出\n") ;
			choose = readChoose(); 
		}
	}
	while (choose == 1);
		
	if (choose == 3)//将数值3定为错误代码
	{
		printf("你所输入的指令有误\n");
		exit (0);
	}
	else 
	{
		return 0;	
	}
}

int readSelect()
{
	int select;
	
	scanf("%d", &select);
	if (select > 0 && select < 3) 
	{
		return (select);
	}
	else 
	{
		return (3);//将数值3定为错误代码 
	}
}

int readYear()
{
	int year;
	
	scanf("%d", &year);
	if (year > 1799)
	{
		return (year);
	}
	else
	{	
		return (3);//将数值3定为错误代码
	}
}

int readChoose()
{
	int choose;
	
	scanf("%d", &choose);
	if (choose > 0 && choose < 3)
	{
		return (choose);
	}
	else
	{
		return (3);//将数值3定为错误代码 
	}
}

int readMonth()
{
	int month;
	
	scanf("%d", &month);
	if (month > 0 && month < 13)
	{
		return (month);
	}
	else
	{
		return (-1);//将数值-1定为错误代码	
	}
}

void printYear(int year, int month1)
{
	printYearTitle(year, month1);
	printYearBody(year, month1);
	printf("\n\n");
}

void printYearTitle(int year, int month1)
{	
	printf("%13d年 %d月\n", year, month1);
	printf("----------------------------\n");
	printf("  日  一  二  三  四  五  六\n");
}

void printYearBody(int year, int month1)
{
	printMonthBody(year, month1);
	printf("\n");
}

void printMonth(int year, int month)
{
	printMonthTitle(year, month);
	printMonthBody(year, month);
}

void printMonthTitle(int year, int month)
{
	printf("%13d年 %d月\n", year, month);
	printf("----------------------------\n");
	printf("  日  一  二  三  四  五  六\n");	
}

void printMonthBody(int year, int month)
{
	int weekday, a, day, ndays, b, k;
	
	weekday = getStartDay(year, month);
	ndays = (getNumberOfDaysInMonth(year, month));
	a = weekday % 7;
	b = a * 4;
	
	for (k = 1; k <= b; k++)
	{
		printf(" ");
	}
	for (day = 1; day <= ndays; day++)
	{
		printf("%4d", day);
		if (a == 6)
		{
			printf("\n");
		}
		a = (a + 1) % 7;
	}
}

int getStartDay(int year, int month)
{
	int weekday;
	
	weekday = (getTotalNumberOfDays(year, month)) % 7 + 3;
	return weekday;
}

int getTotalNumberOfDays(int year, int month)
{
	int i, j, d_days = 0, a = 0, b = 0;
	
	for (j = 1; j < month; j++)
	{
		d_days = d_days + (getNumberOfDaysInMonth(year, j));
	}
	for (i = 1800; i < year; i++)
	{
		if (isLeapYear(i)) 
		{
			a += 1;
		}
		else
		{
			b += 1;
		}
	}
	d_days = d_days + a * 366 + b * 365;
	return d_days;
}

int getNumberOfDaysInMonth(int year, int month)
{
	if (month == 2 || month == 4 || month == 6 || month == 9 || month == 11)
	{
		if (month == 2)
		{
			if (isLeapYear(year))
			{
				return (29);
			}
			else
			{
				return (28);
			}	
		}
		else
		{
			return (30);
		}
	}
	else
	{
		return (31);
	}
}

int isLeapYear(int year)
{
	if ((year % 4 == 0 && year % 100 != 0) || year % 400 == 0)
	{
		return 1;		
	}
	else
	{
		return 0;
	}
}
