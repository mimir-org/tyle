import { MimirorgUserAm } from "@mimirorg/typelibrary-types";
import { recoverPasswordSchema } from "../../content/forms/auth/restore/components/recoverPasswordSchema";

describe("recoverPasswordSchema tests", () => {
  const t = (key: string) => key;

  it("should reject with password less than 10 characters", async () => {
    const userForm: Partial<MimirorgUserAm> = { password: "somesmall" };
    await expect(recoverPasswordSchema(t).validateAt("password", userForm)).rejects.toBeTruthy();
  });

  it("should reject with password and confirmPassword not matching", async () => {
    const userForm: Partial<MimirorgUserAm> = {
      password: "passwordprettylong",
      confirmPassword: "passwordprettylong2",
    };
    await expect(recoverPasswordSchema(t).validateAt("confirmPassword", userForm)).rejects.toBeTruthy();
  });
});
