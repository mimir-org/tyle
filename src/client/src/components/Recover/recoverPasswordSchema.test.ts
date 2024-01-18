import { UserRequest } from "types/authentication/userRequest";
import { recoverPasswordSchema } from "./recoverPasswordSchema";

describe("recoverPasswordSchema tests", () => {
  it("should reject with password less than 10 characters", async () => {
    const userForm: Partial<UserRequest> = { password: "somesmall" };
    await expect(recoverPasswordSchema().validateAt("password", userForm)).rejects.toBeTruthy();
  });

  it("should reject with password and confirmPassword not matching", async () => {
    const userForm: Partial<UserRequest> = {
      password: "passwordprettylong",
      confirmPassword: "passwordprettylong2",
    };
    await expect(recoverPasswordSchema().validateAt("confirmPassword", userForm)).rejects.toBeTruthy();
  });
});
