import { UserRequest } from "types/authentication/userRequest";
import { vi } from "vitest";
import { recoverPasswordSchema } from "./recoverPasswordSchema";

describe("recoverPasswordSchema tests", () => {
  const t = vi.fn();

  it("should reject with password less than 10 characters", async () => {
    const userForm: Partial<UserRequest> = { password: "somesmall" };
    await expect(recoverPasswordSchema(t).validateAt("password", userForm)).rejects.toBeTruthy();
  });

  it("should reject with password and confirmPassword not matching", async () => {
    const userForm: Partial<UserRequest> = {
      password: "passwordprettylong",
      confirmPassword: "passwordprettylong2",
    };
    await expect(recoverPasswordSchema(t).validateAt("confirmPassword", userForm)).rejects.toBeTruthy();
  });
});
