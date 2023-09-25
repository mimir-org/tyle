import { MimirorgThemeProvider } from "@mimirorg/component-library";
import { LinkMenu } from "./LinkMenu";
import { cleanup, render, screen } from "@testing-library/react";
import { afterEach, describe, expect } from "vitest";
import { Link } from "common/types/link";
import "@testing-library/jest-dom";
import { userEvent } from "@testing-library/user-event";
import { MemoryRouter } from "react-router-dom";

const menuLinksMock: Link[] = [
  {
    name: "Block",
    path: "form/block"
  },
  {
    name: "Terminal",
    path: "form/terminal"
  },
  {
    name: "Attribute",
    path: "form/attribute"
  },
  {
    name: "Unit",
    path: "form/unit"
  },
  {
    name: "Quantity Datum",
    path: "form/quantityDatum"
  },
  {
    name: "RDS",
    path: "form/rds"
  }
];

const setup = () => {
  const user = userEvent.setup();

  const testComponent = render(
    <MemoryRouter>
      <MimirorgThemeProvider theme={"tyleLight"}>
        <LinkMenu name={"Create"} links={menuLinksMock} justifyContent={"center"} disabled={false} />
      </MimirorgThemeProvider>
    </MemoryRouter>
  );
  const createButton = screen.getByRole("button", { name: "Create" });

  return {
    user,
    createButton,
    ...testComponent
  };
};

describe("Create dropdown menu integration tests", () => {
  afterEach(() => {
    cleanup();
  });

  test("Should display available links", async () => {
    const { createButton, user } = setup();
    await user.click(createButton);

    expect(await screen.findByText("Block")).toBeVisible();
    expect(await screen.findByText("Terminal")).toBeVisible();
  });
});