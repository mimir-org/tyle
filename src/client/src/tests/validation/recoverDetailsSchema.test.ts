import { MimirorgUserAm } from "@mimirorg/typelibrary-types";
import { recoverDetailsSchema } from "../../features/auth/restore/components/recoverDetailsSchema";

describe("recoverDetailsSchema tests", () => {
  const t = (key: string) => key;

  it("should reject without an email", async () => {
    const userForm: Partial<MimirorgUserAm> = { email: "" };
    await expect(recoverDetailsSchema(t).validateAt("email", userForm)).rejects.toBeTruthy();
  });

  it("should reject with illegal email", async () => {
    const userForm: Partial<MimirorgUserAm> = { email: "this@missingtopleveldomain" };
    await expect(recoverDetailsSchema(t).validateAt("email", userForm)).rejects.toBeTruthy();
  });
});
