import { afterEach, describe, expect, test } from "vitest";
import { FilterMenu } from "./FilterMenu";
import { cleanup, render, screen } from "@testing-library/react";
import { MimirorgThemeProvider } from "@mimirorg/component-library";
import "@testing-library/jest-dom";
import { userEvent } from "@testing-library/user-event";

const filterGroupsMock = [
  {
    name: "Entity",
    filters: [
      {
        key: "kind",
        label: "Block",
        value: "BlockLibCm",
      },
      {
        key: "kind",
        label: "Terminal",
        value: "TerminalLibCm",
      },
      {
        key: "kind",
        label: "Attribute",
        value: "AttributeLibCm",
      },
      {
        key: "kind",
        label: "Unit",
        value: "UnitLibCm",
      },
      {
        key: "kind",
        label: "Quantity datum",
        value: "QuantityDatumLibCm",
      },
      {
        key: "kind",
        label: "RDS",
        value: "RdsLibCm",
      },
    ],
  },
  {
    name: "Terminal",
    filters: [
      {
        key: "kind",
        label: "Terminal",
        value: "TerminalLibCm",
      },
    ],
  },
];

const activeFiltersMock = [
  {
    key: "kind",
    label: "Block",
    value: "BlockLibCm",
  },
];

const setup = () => {
  const user = userEvent.setup();
  const mockToggleFilter = vi.fn();

  const testComponent = render(
    <MimirorgThemeProvider theme={"tyleLight"}>
      <FilterMenu
        toggleFilter={mockToggleFilter}
        name={"Filter"}
        filterGroups={filterGroupsMock}
        activeFilters={activeFiltersMock}
      />
    </MimirorgThemeProvider>,
  );
  const filterButton = screen.getByRole("button", { name: "Filter" });

  return {
    user,
    mockToggleFilter,
    filterButton,
    ...testComponent,
  };
};

describe("Filter dropdown menu integration tests", () => {
  afterEach(() => {
    cleanup();
  });

  test("Should display filter dropdown menu", async () => {
    const { filterButton, user } = setup();
    await user.click(filterButton);

    expect(await screen.findByText("Entity")).toBeVisible();
  });

  test("Should expand filters available on each menu item", async () => {
    const { filterButton, user } = setup();
    await user.click(filterButton);

    const accordionButton = screen.getByText("Entity");
    await user.click(accordionButton);
    expect(await screen.findByText("Block")).toBeVisible();
  });

  test("Toggle filter click handler called", async () => {
    const { filterButton, mockToggleFilter, user } = setup();
    await user.click(filterButton);

    const accordionButton = screen.getByText("Entity");
    await user.click(accordionButton);

    const availableFiltersButton = screen.getByText("Block");
    await user.click(availableFiltersButton);
    expect(mockToggleFilter).toHaveBeenCalled();
  });
});
