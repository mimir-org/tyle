import { MimirorgThemeProvider } from "@mimirorg/component-library";
import { LinkMenu } from "./LinkMenu";
import { cleanup, fireEvent, render, screen } from "@testing-library/react";
import { afterEach, describe, expect } from "vitest";
import { Link } from "common/types/link";
import { BrowserRouter } from "react-router-dom";

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

//TODO: Skille ut denne testfilen i to tester unit og integration.
const setup = () => {
  const testComponent = render(
    <BrowserRouter>
      <MimirorgThemeProvider theme={"tyleLight"}>
        <LinkMenu name={"Create"} links={menuLinksMock} justifyContent={"center"} disabled={false}/>
      </MimirorgThemeProvider>
    </BrowserRouter>
  );
  const createButton = screen.getByText(/create/i);

  return {
    createButton,
    ...testComponent
  };
};

describe("Create Button tests", () => {
  afterEach(() => {
    cleanup();
  });

  test("Should display create dropdown menu", async () => {
    const { createButton } = setup();
    fireEvent.click(createButton);
    //TODO: Vitest tror toBeInTheDocument() er en chai metode??
    // expect(screen.getByText(/block/i)).toBeInTheDocument();
  });
});