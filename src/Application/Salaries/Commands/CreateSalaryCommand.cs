using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MediatR;
using LogOT.Application.Common.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LogOT.Application.Salaries.Commands;

public record CreateSalaryCommand : IRequest<SalaryDto>
{
    public double Income { get; set; }
    public string InsuranceType { get; set; }
    public double CustomSalary { get; set; }
    public double Number_Of_Dependents { get; set; }
    public string SalaryType { get; set; }

}

public class CreateSalaryCommandHandler : IRequestHandler<CreateSalaryCommand, SalaryDto>
{
    private readonly IApplicationDbContext _context;

    public CreateSalaryCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<SalaryDto> Handle(CreateSalaryCommand request, CancellationToken cancellationToken)
    {
        //khai báo
        double Gross = 0;
        double Net = 0;
        double Exchange_Salary = 0;
        double BHXH_Emp = 0;
        double BHYT_Emp = 0;
        double BHTN_Emp = 0;
        double BHXH_Cmp = 0;
        double BHYT_Cmp = 0;
        double BHTN_Cmp = 0;
        double TNTT = 0;
        double TNCT = 0;
        double TTNCN = 0;
        double CmpSalaryFinal = 0;
        List<double> TTNCN_Detail = new List<double>();
        //chưa có table DetailTaxIncome nên dùng tạm
        double[] mucChiuThue = { 5000000, 10000000, 18000000, 32000000, 52000000, 80000000 };
        double[] thueSuat = { 5, 10, 15, 20, 25, 30, 35 };
        double[] lvMax = new double[mucChiuThue.Length];
        int n = mucChiuThue.Length;
        //double[] TTNCN_Detail = new double[n];
        for (int i = 0; i < n; i++)
        {
            if (i == 0)
            {
                lvMax[i] = mucChiuThue[i] * (thueSuat[i] / 100);
            }
            else
            {
                lvMax[i] = (mucChiuThue[i] - mucChiuThue[i - 1]) * (thueSuat[i] / 100);
            }
        }
        //nếu là lương NET
        if (request.SalaryType == "Net")
        {
            Gross = request.Income;
            //tính các loại bảo hiểm nhân viên phải trả 
            //tính trên lương
            if (request.InsuranceType == "official")
            {
                BHXH_Emp = Gross * 0.08;
                if (BHXH_Emp > 2384000)
                {
                    BHXH_Emp = 2384000;
                }
                BHYT_Emp = Gross * 0.015;
                if (BHYT_Emp > 447000)
                {
                    BHYT_Emp = 447000;
                }
                BHTN_Emp = Gross * 0.01;
                if (BHTN_Emp > 884000)
                {
                    BHTN_Emp = 884000;
                }
            }
            //tính trên số khác
            else
            {
                BHXH_Emp = request.CustomSalary * 0.08;
                if (BHXH_Emp > 2384000)
                {
                    BHXH_Emp = 2384000;
                }
                BHYT_Emp = request.CustomSalary * 0.015;
                if (BHYT_Emp > 447000)
                {
                    BHYT_Emp = 447000;
                }
                BHTN_Emp = request.CustomSalary * 0.01;
                if (BHTN_Emp > 884000)
                {
                    BHTN_Emp = 884000;
                }
            }

            //tính thu nhập trước thuế
            TNTT = Gross - BHXH_Emp - BHYT_Emp - BHTN_Emp;
            //tính thu nhập chịu thuế
            TNCT = (double)(TNTT - 11000000 - request.Number_Of_Dependents * 4400000);
            // nếu thu nhập chịu thuế > 0 thì tính thuế thu nhập cá nhân
            if (TNCT > 0)
            {
                // Nếu TNCT nhỏ hơn mức chịu thuế đầu tiên
                if (TNCT <= mucChiuThue[0])
                {
                    // Tính thuế cho mức thu nhập chịu thuế là TNCT
                    TTNCN = TNCT * (thueSuat[0] / 100);
                    // Thêm giá trị TTNCN vào danh sách TTNCN_Detail
                    TTNCN_Detail.Add(TTNCN);
                }
                else
                {
                    for (int i = 1; i < n; i++)
                    {
                        if (TNCT <= mucChiuThue[i])
                        {
                            TTNCN = (TNCT - mucChiuThue[i - 1]) * (thueSuat[i] / 100);
                            for (int j = 0; j < i; j++)
                            {
                                TTNCN += lvMax[j];
                                TTNCN_Detail.Add(TTNCN);
                            }
                            TTNCN_Detail.Add(TTNCN);
                            break;
                        }
                    }
                    if (TNCT > mucChiuThue[n - 1])
                    {
                        TTNCN = (TNCT - mucChiuThue[n - 1]) * (thueSuat[n] / 100);
                        for (int j = 0; j < n; j++)
                        {
                            TTNCN += lvMax[j];
                        }
                        TTNCN_Detail.Add(TTNCN);
                    }
                }
            }
            else
            {
                TTNCN_Detail.Add(TTNCN);
            }

            //tính lương cuối
            Net = TNTT - TTNCN;

            //tính các loại bảo hiểm công ty phải trả 
            BHXH_Cmp = Gross * 0.175;
            if (BHXH_Cmp > 5215000)
            {
                BHXH_Cmp = 5215000;
            }
            BHYT_Cmp = Gross * 0.03;
            if (BHYT_Cmp > 894000)
            {
                BHYT_Cmp = 894000;
            }
            BHTN_Cmp = Gross * 0.01;
            if (BHTN_Cmp > 884000)
            {
                BHTN_Cmp = 884000;
            }
            //tính lương phía công ty phải chi trả
            CmpSalaryFinal = Gross + BHXH_Cmp + BHYT_Cmp + BHTN_Cmp;
        }
        //nếu là lương GROSS
        else
        {
            //tính lương Net = thu nhập - ngày nghỉ
            Net = request.Income;
            //tính lương quy đổi
            Exchange_Salary = (double)(Net - 11000000 - request.Number_Of_Dependents * 4400000);

            //sử dụng bảng quy đổi để tính thu nhập chịu thuế
            if (Exchange_Salary > 61850000)
            {
                TNCT = (Exchange_Salary - 9850000) / 0.65;
            }
            else if (Exchange_Salary <= 61850000 && Exchange_Salary > 42250000)
            {
                TNCT = (Exchange_Salary - 5850000) / 0.7;
            }
            else if (Exchange_Salary <= 42250000 && Exchange_Salary > 27250000)
            {
                TNCT = (Exchange_Salary - 3250000) / 0.75;
            }
            else if (Exchange_Salary <= 27250000 && Exchange_Salary > 16050000)
            {
                TNCT = (Exchange_Salary - 1650000) / 0.8;
            }
            else if (Exchange_Salary <= 16050000 && Exchange_Salary > 9250000)
            {
                TNCT = (Exchange_Salary - 750000) / 0.85;
            }
            else if (Exchange_Salary <= 9250000 && Exchange_Salary > 4750000)
            {
                TNCT = (Exchange_Salary - 250000) / 0.9;
            }
            else if (Exchange_Salary <= 4750000)
            {
                TNCT = Exchange_Salary / 0.95;
            }

            // nếu thu nhập chịu thuế > 0 thì tính thuế thu nhập cá nhân
            if (TNCT > 0)
            {
                // Nếu TNCT nhỏ hơn mức chịu thuế đầu tiên
                if (TNCT <= mucChiuThue[0])
                {
                    // Tính thuế cho mức thu nhập chịu thuế là TNCT
                    TTNCN = TNCT * (thueSuat[0] / 100);
                    // Thêm giá trị TTNCN vào danh sách TTNCN_Detail
                    TTNCN_Detail.Add(TTNCN);
                }
                else
                {
                    for (int i = 1; i < n; i++)
                    {
                        if (TNCT <= mucChiuThue[i])
                        {
                            TTNCN = (TNCT - mucChiuThue[i - 1]) * (thueSuat[i] / 100);
                            for (int j = 0; j < i; j++)
                            {
                                TTNCN += lvMax[j];
                            }
                            TTNCN_Detail.Add(TTNCN);
                            break;
                        }
                    }
                    if (TNCT > mucChiuThue[n - 1])
                    {
                        TTNCN = (TNCT - mucChiuThue[n - 1]) * (thueSuat[n] / 100);
                        for (int j = 0; j < n; j++)
                        {
                            TTNCN += lvMax[j];
                        }
                        TTNCN_Detail.Add(TTNCN);
                    }
                }
            }
            else
            {
                TTNCN_Detail.Add(TTNCN);
            }
            //tính thu nhập trước thuế bằng thu nhập + thuế thu nhập cá nhân
            TNTT = Net + TTNCN;
            //tính các loại bảo hiểm nhân viên phải trả 
            //tính trên lương
            if (request.InsuranceType == "official")
            {
                //chia cho 0.895 để ra lương gross, từ lương gross tính ra các BH
                Gross = TNTT / 0.895;
                BHXH_Emp = Gross * 0.08;
                if (BHXH_Emp > 2384000)
                {
                    BHXH_Emp = 2384000;
                }
                BHYT_Emp = Gross * 0.015;
                if (BHYT_Emp > 447000)
                {
                    BHYT_Emp = 447000;
                }
                BHTN_Emp = Gross * 0.01;
                if (BHTN_Emp > 884000)
                {
                    BHTN_Emp = 884000;
                }
            }
            //tính trên số khác
            else
            {
                //tính các loại bảo hiểm nhân viên phải trả 
                BHXH_Emp = request.CustomSalary * 0.08;
                if (BHXH_Emp > 2384000)
                {
                    BHXH_Emp = 2384000;
                }
                BHYT_Emp = request.CustomSalary * 0.015;
                if (BHYT_Emp > 447000)
                {
                    BHYT_Emp = 447000;
                }
                BHTN_Emp = request.CustomSalary * 0.01;
                if (BHTN_Emp > 884000)
                {
                    BHTN_Emp = 884000;
                }
            }
            //tính lương cuối
            Gross = TNTT + BHXH_Emp + BHYT_Emp + BHTN_Emp;

            //tính các loại bảo hiểm công ty phải trả 
            BHXH_Cmp = Gross * 0.175;
            if (BHXH_Cmp > 5215000)
            {
                BHXH_Cmp = 5215000;
            }
            BHYT_Cmp = Gross * 0.03;
            if (BHYT_Cmp > 894000)
            {
                BHYT_Cmp = 894000;
            }
            BHTN_Cmp = Gross * 0.01;
            if (BHTN_Cmp > 884000)
            {
                BHTN_Cmp = 884000;
            }
            //tính lương phía công ty phải chi trả
            CmpSalaryFinal = Gross + BHXH_Cmp + BHYT_Cmp + BHTN_Cmp;
        }
        if (TNCT < 0)
        {
            TNCT = 0;
        }
        var Salary = new SalaryDto
        {
            Gross = Gross,
            BHXH_Emp = BHXH_Emp,
            BHYT_Emp = BHYT_Emp,
            BHTN_Emp = BHTN_Emp,
            TNTT = TNTT,
            Dependent_Deduction = request.Number_Of_Dependents * 4400000,
            TNCT = TNCT,
            TTNCN = TTNCN,
            Net = Net,
            TTNCN_Detail = TTNCN_Detail,
            BHXH_Comp = BHXH_Cmp,
            BHYT_Comp = BHYT_Cmp,
            BHTN_Comp = BHTN_Cmp,
            Total_Cmp_Salary = CmpSalaryFinal,
        };
        return Salary;
    }
}