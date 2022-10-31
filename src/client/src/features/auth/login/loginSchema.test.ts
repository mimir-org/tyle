import { MimirorgAuthenticateAm } from "@mimirorg/typelibrary-types";
import { loginSchema } from "features/auth/login/loginSchema";

describe("loginSchema tests", () => {
  const t = (key: string) => key;

  it("should reject without an email", async () => {
    const userForm: Partial<MimirorgAuthenticateAm> = { email: "" };
    await expect(loginSchema(t).validateAt("email", userForm)).rejects.toBeTruthy();
  });

  it("should reject with illegal email", async () => {
    const userForm: Partial<MimirorgAuthenticateAm> = { email: "this@missingtopleveldomain" };
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
