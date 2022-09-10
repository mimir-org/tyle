import { hasAccess } from "../utils/hasAccess";
import { MimirorgPermission, MimirorgUserCm } from "@mimirorg/typelibrary-types";

test("manager has correct accesses", () => {
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

test("approver has correct accesses", () => {
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

test("delete has correct accesses", () => {
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

test("write has correct accesses", () => {
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

test("read has correct accesses", () => {
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
