#include<stdio.h>
#include<stdlib.h>
int readYear();//��ȡ�û���������
int readMonth();//��ȡ�û�������·�
int readSelect();//��ȡ�û���ѡ�� 
int readChoose();//��ȡ�û��Ƿ�������в�ѯ 
void printMonth(int year, int month) ;//��ӡ����
void printMonthTitle(int year, int month) ;//��ӡ��������
void printMonthBody(int year, int month) ;//��ӡ�������� 
void printYear(int year, int month1);//��ӡ����
void printYearTitle(int year, int month1);//��ӡ��������
void printYearBody(int year, int month1);//��ӡ�������� 
int getStartDay(int year, int month) ;//��ȡyear��month�µĵ�һ���������
int getTotalNumberOfDays(int year, int month) ;//��ȡ��1800��1��1��(������)��year��month��1�յ����������
int getNumberOfDaysInMonth(int year, int month) ;//��ȡyear��month�µ�������
int isLeapYear(int year) ;//�ж�year���Ƿ�Ϊ���꣨���Ϊ���꣬�򷵻�1�����򷵻�0��

int main()
{
	int year, month, select, i, choose, month1;
	
	printf("��ӭʹ����������� ^_^\n"); 
	do
	{
		do
		{	
			printf("��ѡ������Ҫ��ѯ��ģʽ��\n  1������    2������\n");
			select = readSelect();
			while (select == 3)
			{
				printf("���������ģʽָ������\n");
				printf("������������Ҫ��ѯ��ģʽ��\n  1������    2������\n");
				select = readSelect();
			}
			if (select != 3)
			{
				printf("������Ҫ��ѯ����ݣ�(����2016  ��Χ��1800-����)\n");
				year = readYear();
				while (year == 3)//����ֵ3��Ϊ������� 
				{
					printf("�����������ݳ������Ʒ�Χ\n");
					printf("������������Ҫ��ѯ�����  (����2016  ��Χ��1800-����)\n"); 
					year = readYear();
				}
				while (year != 3)//����ֵ3��Ϊ������� 
				{
					if ( select == 2)
					{
						printf("������Ҫ��ѯ���·ݣ�����12  ��Χ��1-12\n");
						month = readMonth();
						while (month == -1)//����ֵ-1��Ϊ������� 
						{
							printf("����������·ݳ������Ʒ�Χ\n");
							printf("������������Ҫ��ѯ���·�  (����12  ��Χ��1-12)\n"); 
							month = readMonth();
						}
						while (month != -1)
						{
							printMonth(year, month);
							printf("\n\n");//ʹ�������� 
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
			printf("���ʻ�Ҫ������ѯ��\n  1������  2���˳�\n");
			choose = readChoose();
		}
		while (choose == 1);
		if (choose == 3) //����ֵ3��Ϊ�������
		{
			printf("���������ָ������\n�㻹ʣ��1���޸Ļ���\n");
			printf("����������ָ��\n   1������  2���˳�\n") ;
			choose = readChoose(); 
		}
	}
	while (choose == 1);
		
	if (choose == 3)//����ֵ3��Ϊ�������
	{
		printf("���������ָ������\n");
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
		return (3);//����ֵ3��Ϊ������� 
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
		return (3);//����ֵ3��Ϊ�������
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
		return (3);//����ֵ3��Ϊ������� 
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
		return (-1);//����ֵ-1��Ϊ�������	
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
	printf("%13d�� %d��\n", year, month1);
	printf("----------------------------\n");
	printf("  ��  һ  ��  ��  ��  ��  ��\n");
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
	printf("%13d�� %d��\n", year, month);
	printf("----------------------------\n");
	printf("  ��  һ  ��  ��  ��  ��  ��\n");	
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
