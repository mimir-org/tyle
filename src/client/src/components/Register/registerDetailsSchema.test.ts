import { UserRequest } from "types/authentication/userRequest";
import { registerDetailsSchema } from "./registerDetailsSchema";

describe("registerDetailsSchema tests", () => {
  it("should reject without a firstName", async () => {
    const userForm: Partial<UserRequest> = { firstName: "" };
    await expect(registerDetailsSchema().validateAt("firstName", userForm)).rejects.toBeTruthy();
  });

  it("should reject without a lastName", async () => {
    const userForm: Partial<UserRequest> = { lastName: "" };
    await expect(registerDetailsSchema().validateAt("lastName", userForm)).rejects.toBeTruthy();
  });

  it("should reject without an email", async () => {
    const userForm: Partial<UserRequest> = { email: "" };
    await expect(registerDetailsSchema().validateAt("email", userForm)).rejects.toBeTruthy();
  });

  it("should reject with illegal email", async () => {
    const userForm: Partial<UserRequest> = { email: "no-at-character" };
    await expect(registerDetailsSchema().validateAt("email", userForm)).rejects.toBeTruthy();
  });

  it("should reject with password less than 10 characters", async () => {
    const userForm: Partial<UserRequest> = { password: "somesmall" };
    await expect(registerDetailsSchema().validateAt("password", userForm)).rejects.toBeTruthy();
  });

  it("should reject with password and confirmPassword not matching", async () => {
    const userForm: Partial<UserRequest> = {
      password: "passwordprettylong",
      confirmPassword: "passwordprettylong2",
    };
    await expect(registerDetailsSchema().validateAt("confirmPassword", userForm)).rejects.toBeTruthy();
  });
});
