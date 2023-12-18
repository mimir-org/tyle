import { AuthenticateRequest } from "types/authentication/authenticateRequest";
import { loginSchema } from "./loginSchema";

describe("loginSchema tests", () => {
  it("should reject without an email", async () => {
    const userForm: Partial<AuthenticateRequest> = { email: "" };
    await expect(loginSchema().validateAt("email", userForm)).rejects.toBeTruthy();
  });

  it("should reject with illegal email", async () => {
    const userForm: Partial<AuthenticateRequest> = { email: "no-at-character" };
    await expect(loginSchema().validateAt("email", userForm)).rejects.toBeTruthy();
  });

  it("should reject without password", async () => {
    const userForm: Partial<AuthenticateRequest> = { password: "" };
    await expect(loginSchema().validateAt("password", userForm)).rejects.toBeTruthy();
  });

  it("should reject without authentication code", async () => {
    const userForm: Partial<AuthenticateRequest> = { code: "" };
    await expect(loginSchema().validateAt("code", userForm)).rejects.toBeTruthy();
  });

  it("should reject authentication code with letters", async () => {
    const userForm: Partial<AuthenticateRequest> = { code: "12312a" };
    await expect(loginSchema().validateAt("code", userForm)).rejects.toBeTruthy();
  });
});
