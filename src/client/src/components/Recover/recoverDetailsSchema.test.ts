import { UserRequest } from "types/authentication/userRequest";
import { recoverDetailsSchema } from "./recoverDetailsSchema";

describe("recoverDetailsSchema tests", () => {
  it("should reject without an email", async () => {
    const userForm: Partial<UserRequest> = { email: "" };
    await expect(recoverDetailsSchema().validateAt("email", userForm)).rejects.toBeTruthy();
  });

  it("should reject with illegal email", async () => {
    const userForm: Partial<UserRequest> = { email: "no-at-character" };
    await expect(recoverDetailsSchema().validateAt("email", userForm)).rejects.toBeTruthy();
  });
});
