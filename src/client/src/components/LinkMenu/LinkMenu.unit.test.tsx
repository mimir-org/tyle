import { MimirorgThemeProvider } from "@mimirorg/component-library";
import "@testing-library/jest-dom";
import { cleanup, render, screen } from "@testing-library/react";
import { Link } from "types/link";
import { afterEach, describe, expect } from "vitest";
import LinkMenu from "./LinkMenu";

const menuLinksMock: Link[] = [
  {
    name: "Block",
    path: "form/block",
  },
  {
    name: "Terminal",
    path: "form/terminal",
  },
  {
    name: "Attribute",
    path: "form/attribute",
  },
  {
    name: "Unit",
    path: "form/unit",
  },
  {
    name: "Quantity Datum",
    path: "form/quantityDatum",
  },
  {
    name: "RDS",
    path: "form/rds",
  },
];

const setup = () => {
  const testComponent = render(
    <MimirorgThemeProvider theme={"tyleLight"}>
      <LinkMenu name={"Create"} links={menuLinksMock} justifyContent={"center"} disabled={false} />
    </MimirorgThemeProvider>,
  );
  const createButton = screen.getByRole("button", { name: "Create" });

  return {
    createButton,
    ...testComponent,
  };
};

describe("Create dropdown menu unit tests", () => {
  afterEach(() => {
    cleanup();
  });

  test("Did component render", () => {
    const { createButton } = setup();
    expect(createButton).toBeVisible();
  });
});
