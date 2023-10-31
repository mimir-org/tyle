import { MimirorgUserAm } from "@mimirorg/typelibrary-types";
import { registerDetailsSchema } from "components/Register/registerDetailsSchema";
import { vi } from "vitest";

describe("registerDetailsSchema tests", () => {
  const t = vi.fn();

  it("should reject without a firstName", async () => {
    const userForm: Partial<MimirorgUserAm> = { firstName: "" };
    await expect(registerDetailsSchema(t).validateAt("firstName", userForm)).rejects.toBeTruthy();
  });

  it("should reject without a lastName", async () => {
    const userForm: Partial<MimirorgUserAm> = { lastName: "" };
    await expect(registerDetailsSchema(t).validateAt("lastName", userForm)).rejects.toBeTruthy();
  });

  it("should reject without an email", async () => {
    const userForm: Partial<MimirorgUserAm> = { email: "" };
    await expect(registerDetailsSchema(t).validateAt("email", userForm)).rejects.toBeTruthy();
  });

  it("should reject with illegal email", async () => {
    const userForm: Partial<MimirorgUserAm> = { email: "no-at-character" };
    await expect(registerDetailsSchema(t).validateAt("email", userForm)).rejects.toBeTruthy();
  });

  it("should reject with password less than 10 characters", async () => {
    const userForm: Partial<MimirorgUserAm> = { password: "somesmall" };
    await expect(registerDetailsSchema(t).validateAt("password", userForm)).rejects.toBeTruthy();
  });

  it("should reject with password and confirmPassword not matching", async () => {
    const userForm: Partial<MimirorgUserAm> = {
      password: "passwordprettylong",
      confirmPassword: "passwordprettylong2",
    };
    await expect(registerDetailsSchema(t).validateAt("confirmPassword", userForm)).rejects.toBeTruthy();
  });

  it("should reject without an organization if there are any available", async () => {
    const userForm: Partial<MimirorgUserAm> = {};
    await expect(registerDetailsSchema(t, true).validateAt("companyId", userForm)).rejects.toBeTruthy();
  });
});
