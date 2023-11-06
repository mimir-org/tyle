import { UserRequest } from "types/authentication/userRequest";
import { vi } from "vitest";
import { recoverDetailsSchema } from "./recoverDetailsSchema";

describe("recoverDetailsSchema tests", () => {
  const t = vi.fn();

  it("should reject without an email", async () => {
    const userForm: Partial<UserRequest> = { email: "" };
    await expect(recoverDetailsSchema(t).validateAt("email", userForm)).rejects.toBeTruthy();
  });

  it("should reject with illegal email", async () => {
    const userForm: Partial<UserRequest> = { email: "no-at-character" };
    await expect(recoverDetailsSchema(t).validateAt("email", userForm)).rejects.toBeTruthy();
  });
});
