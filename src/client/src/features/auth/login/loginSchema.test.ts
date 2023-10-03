import { MimirorgAuthenticateAm } from "@mimirorg/typelibrary-types";
import { loginSchema } from "features/auth/login/loginSchema";
import { vi } from "vitest";

describe("loginSchema tests", () => {
  const t = vi.fn();

  it("should reject without an email", async () => {
    const userForm: Partial<MimirorgAuthenticateAm> = { email: "" };
    await expect(loginSchema(t).validateAt("email", userForm)).rejects.toBeTruthy();
  });

  it("should reject with illegal email", async () => {
    const userForm: Partial<MimirorgAuthenticateAm> = { email: "no-at-character" };
    await expect(loginSchema(t).validateAt("email", userForm)).rejects.toBeTruthy();
  });

  it("should reject without password", async () => {
    const userForm: Partial<MimirorgAuthenticateAm> = { password: "" };
    await expect(loginSchema(t).validateAt("password", userForm)).rejects.toBeTruthy();
  });

  it("should reject without authentication code", async () => {
    const userForm: Partial<MimirorgAuthenticateAm> = { code: "" };
    await expect(loginSchema(t).validateAt("code", userForm)).rejects.toBeTruthy();
  });

  it("should reject authentication code with letters", async () => {
    const userForm: Partial<MimirorgAuthenticateAm> = { code: "12312a" };
    await expect(loginSchema(t).validateAt("code", userForm)).rejects.toBeTruthy();
  });
});
