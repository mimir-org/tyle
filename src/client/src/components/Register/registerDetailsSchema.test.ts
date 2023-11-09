import { UserRequest } from "types/authentication/userRequest";
import { vi } from "vitest";
import { registerDetailsSchema } from "./registerDetailsSchema";

describe("registerDetailsSchema tests", () => {
  const t = vi.fn();

  it("should reject without a firstName", async () => {
    const userForm: Partial<UserRequest> = { firstName: "" };
    await expect(registerDetailsSchema(t).validateAt("firstName", userForm)).rejects.toBeTruthy();
  });

  it("should reject without a lastName", async () => {
    const userForm: Partial<UserRequest> = { lastName: "" };
    await expect(registerDetailsSchema(t).validateAt("lastName", userForm)).rejects.toBeTruthy();
  });

  it("should reject without an email", async () => {
    const userForm: Partial<UserRequest> = { email: "" };
    await expect(registerDetailsSchema(t).validateAt("email", userForm)).rejects.toBeTruthy();
  });

  it("should reject with illegal email", async () => {
    const userForm: Partial<UserRequest> = { email: "no-at-character" };
    await expect(registerDetailsSchema(t).validateAt("email", userForm)).rejects.toBeTruthy();
  });

  it("should reject with password less than 10 characters", async () => {
    const userForm: Partial<UserRequest> = { password: "somesmall" };
    await expect(registerDetailsSchema(t).validateAt("password", userForm)).rejects.toBeTruthy();
  });

  it("should reject with password and confirmPassword not matching", async () => {
    const userForm: Partial<UserRequest> = {
      password: "passwordprettylong",
      confirmPassword: "passwordprettylong2",
    };
    await expect(registerDetailsSchema(t).validateAt("confirmPassword", userForm)).rejects.toBeTruthy();
  });
});
