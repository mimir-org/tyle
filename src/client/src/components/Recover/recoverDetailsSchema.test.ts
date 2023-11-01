import { MimirorgUserAm } from "@mimirorg/typelibrary-types";
import { vi } from "vitest";
import { recoverDetailsSchema } from "./recoverDetailsSchema";

describe("recoverDetailsSchema tests", () => {
  const t = vi.fn();

  it("should reject without an email", async () => {
    const userForm: Partial<MimirorgUserAm> = { email: "" };
    await expect(recoverDetailsSchema(t).validateAt("email", userForm)).rejects.toBeTruthy();
  });

  it("should reject with illegal email", async () => {
    const userForm: Partial<MimirorgUserAm> = { email: "no-at-character" };
    await expect(recoverDetailsSchema(t).validateAt("email", userForm)).rejects.toBeTruthy();
  });
});
