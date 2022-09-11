import { hasAccess, filterCompanyList } from "../utils/hasAccess";
import { MimirorgCompanyCm, MimirorgPermission, MimirorgUserCm } from "@mimirorg/typelibrary-types";

describe("hasAccess tests", () => {
  test("undefined user returns false", () => {
    expect(hasAccess({} as MimirorgUserCm, 0, MimirorgPermission.Manage)).toBe(false);
  });

  test("missing company id returns false", () => {
    const user = {
      id: "12345",
      firstName: "Hans",
      lastName: "Hasen",
      email: "hans.hansen@runir.net",
      phoneNumber: "12345678",
      permissions: {},
    } as MimirorgUserCm;

    expect(hasAccess(user, 0, MimirorgPermission.Manage)).toBe(false);
  });

  test("empty permissions validates ok", () => {
    const user = {
      id: "12345",
      firstName: "Hans",
      lastName: "Hasen",
      email: "hans.hansen@runir.net",
      phoneNumber: "12345678",
      permissions: {},
    } as MimirorgUserCm;

    expect(hasAccess(user, 1, MimirorgPermission.Manage)).toBe(false);
    expect(hasAccess(user, 1, MimirorgPermission.Approve)).toBe(false);
    expect(hasAccess(user, 1, MimirorgPermission.Delete)).toBe(false);
    expect(hasAccess(user, 1, MimirorgPermission.Write)).toBe(false);
    expect(hasAccess(user, 1, MimirorgPermission.Read)).toBe(false);
  });

  test("manage validates ok", () => {
    const user = {
      id: "12345",
      firstName: "Hans",
      lastName: "Hasen",
      email: "hans.hansen@runir.net",
      phoneNumber: "12345678",
      permissions: {
        "1": MimirorgPermission.Manage,
      },
    } as MimirorgUserCm;

    expect(hasAccess(user, 1, MimirorgPermission.Manage)).toBe(true);
    expect(hasAccess(user, 1, MimirorgPermission.Approve)).toBe(true);
    expect(hasAccess(user, 1, MimirorgPermission.Delete)).toBe(true);
    expect(hasAccess(user, 1, MimirorgPermission.Write)).toBe(true);
    expect(hasAccess(user, 1, MimirorgPermission.Read)).toBe(true);
  });

  test("approve validates ok", () => {
    const user = {
      id: "12345",
      firstName: "Hans",
      lastName: "Hasen",
      email: "hans.hansen@runir.net",
      phoneNumber: "12345678",
      permissions: {
        "1": MimirorgPermission.Approve,
      },
    } as MimirorgUserCm;

    expect(hasAccess(user, 1, MimirorgPermission.Manage)).toBe(false);
    expect(hasAccess(user, 1, MimirorgPermission.Approve)).toBe(true);
    expect(hasAccess(user, 1, MimirorgPermission.Delete)).toBe(true);
    expect(hasAccess(user, 1, MimirorgPermission.Write)).toBe(true);
    expect(hasAccess(user, 1, MimirorgPermission.Read)).toBe(true);
  });

  test("delete validates ok", () => {
    const user = {
      id: "12345",
      firstName: "Hans",
      lastName: "Hasen",
      email: "hans.hansen@runir.net",
      phoneNumber: "12345678",
      permissions: {
        "1": MimirorgPermission.Delete,
      },
    } as MimirorgUserCm;

    expect(hasAccess(user, 1, MimirorgPermission.Manage)).toBe(false);
    expect(hasAccess(user, 1, MimirorgPermission.Approve)).toBe(false);
    expect(hasAccess(user, 1, MimirorgPermission.Delete)).toBe(true);
    expect(hasAccess(user, 1, MimirorgPermission.Write)).toBe(true);
    expect(hasAccess(user, 1, MimirorgPermission.Read)).toBe(true);
  });

  test("write validates ok", () => {
    const user = {
      id: "12345",
      firstName: "Hans",
      lastName: "Hasen",
      email: "hans.hansen@runir.net",
      phoneNumber: "12345678",
      permissions: {
        "1": MimirorgPermission.Write,
      },
    } as MimirorgUserCm;

    expect(hasAccess(user, 1, MimirorgPermission.Manage)).toBe(false);
    expect(hasAccess(user, 1, MimirorgPermission.Approve)).toBe(false);
    expect(hasAccess(user, 1, MimirorgPermission.Delete)).toBe(false);
    expect(hasAccess(user, 1, MimirorgPermission.Write)).toBe(true);
    expect(hasAccess(user, 1, MimirorgPermission.Read)).toBe(true);
  });

  test("read validates ok", () => {
    const user = {
      id: "12345",
      firstName: "Hans",
      lastName: "Hasen",
      email: "hans.hansen@runir.net",
      phoneNumber: "12345678",
      permissions: {
        "1": MimirorgPermission.Read,
      },
    } as MimirorgUserCm;

    expect(hasAccess(user, 1, MimirorgPermission.Manage)).toBe(false);
    expect(hasAccess(user, 1, MimirorgPermission.Approve)).toBe(false);
    expect(hasAccess(user, 1, MimirorgPermission.Delete)).toBe(false);
    expect(hasAccess(user, 1, MimirorgPermission.Write)).toBe(false);
    expect(hasAccess(user, 1, MimirorgPermission.Read)).toBe(true);
  });

  test("filtered companies returns correct list for one company", () => {
    const user = {
      id: "12345",
      firstName: "Hans",
      lastName: "Hasen",
      email: "hans.hansen@runir.net",
      phoneNumber: "12345678",
      permissions: {
        "1": MimirorgPermission.Write,
        "2": MimirorgPermission.Read,
        "3": MimirorgPermission.Read,
      },
    } as MimirorgUserCm;

    const companies = [
      {
        id: 1,
        name: "Company A",
      },
      {
        id: 2,
        name: "Company B",
      },
      {
        id: 3,
        name: "Company C",
      },
    ] as MimirorgCompanyCm[];

    expect(filterCompanyList(companies, user, MimirorgPermission.Write).length).toBe(1);
    expect(filterCompanyList(companies, user, MimirorgPermission.Write).filter((x) => x.id === 1)[0].id).toBe(1);
    expect(filterCompanyList(companies, user, MimirorgPermission.Write).filter((x) => x.id === 1)[0].name).toBe(
      "Company A"
    );
  });

  test("filtered companies returns correct list for two companies", () => {
    const user = {
      id: "12345",
      firstName: "Hans",
      lastName: "Hasen",
      email: "hans.hansen@runir.net",
      phoneNumber: "12345678",
      permissions: {
        "1": MimirorgPermission.Manage,
        "2": MimirorgPermission.Approve,
        "3": MimirorgPermission.Read,
      },
    } as MimirorgUserCm;

    const companies = [
      {
        id: 1,
        name: "Company A",
      },
      {
        id: 2,
        name: "Company B",
      },
      {
        id: 3,
        name: "Company C",
      },
    ] as MimirorgCompanyCm[];

    expect(filterCompanyList(companies, user, MimirorgPermission.Approve).length).toBe(2);
    expect(filterCompanyList(companies, user, MimirorgPermission.Approve).filter((x) => x.id === 1)[0].id).toBe(1);
    expect(filterCompanyList(companies, user, MimirorgPermission.Approve).filter((x) => x.id === 2)[0].id).toBe(2);
    expect(filterCompanyList(companies, user, MimirorgPermission.Write).filter((x) => x.id === 1)[0].name).toBe(
      "Company A"
    );
    expect(filterCompanyList(companies, user, MimirorgPermission.Write).filter((x) => x.id === 2)[0].name).toBe(
      "Company B"
    );
  });

  test("filtered companies returns empty list with no permissions", () => {
    const user = {
      id: "12345",
      firstName: "Hans",
      lastName: "Hasen",
      email: "hans.hansen@runir.net",
      phoneNumber: "12345678",
      permissions: {},
    } as MimirorgUserCm;

    const companies = [
      {
        id: 1,
        name: "Company A",
      },
      {
        id: 2,
        name: "Company B",
      },
      {
        id: 3,
        name: "Company C",
      },
    ] as MimirorgCompanyCm[];

    expect(filterCompanyList(companies, user, MimirorgPermission.Approve).length).toBe(0);
  });

  test("filtered companies returns empty list with no companies", () => {
    const user = {
      id: "12345",
      firstName: "Hans",
      lastName: "Hasen",
      email: "hans.hansen@runir.net",
      phoneNumber: "12345678",
      permissions: {
        "1": MimirorgPermission.Manage,
        "2": MimirorgPermission.Approve,
        "3": MimirorgPermission.Read,
      },
    } as MimirorgUserCm;

    const companies = [] as MimirorgCompanyCm[];

    expect(filterCompanyList(companies, user, MimirorgPermission.Approve).length).toBe(0);
  });
});
